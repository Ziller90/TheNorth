using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGrid : MonoBehaviour
{
    public Transform gridStartPosition;
    public float nextCellDistance;
    public Transform trashCan;
    public float trashCanRange;

    [HideInInspector] public Vector3 leftTopCorner;
    [HideInInspector] public Vector3 rightBottomCorner;

    [SerializeField] Container container;
    [SerializeField] ItemsCollector itemsCollector;
    [SerializeField] GameObject iconSize1;
    [SerializeField] GameObject iconSize2;
    [SerializeField] GameObject iconSize3;
    [SerializeField] GameObject iconSize4;
    [SerializeField] float gridRangeFactor;
    [SerializeField] Transform gridNextPosition;
    [SerializeField] Transform itemsCollection;

    private void Awake()
    {
        nextCellDistance = Vector3.Distance(gridStartPosition.position, gridNextPosition.position);
    }
    void Start()
    {
        leftTopCorner = new Vector3(gridStartPosition.position.x - nextCellDistance * gridRangeFactor, gridStartPosition.position.y + nextCellDistance * gridRangeFactor, 0);
        Vector3 lastPointVector = GetPointVector(new Coordinates(container.ySize - 1, container.xSize - 1));
        rightBottomCorner = new Vector3(lastPointVector.x + nextCellDistance * gridRangeFactor, lastPointVector.y - nextCellDistance * gridRangeFactor, 0);
    }
    private void OnEnable()
    {
        InstantiateItemsIcons();
    }
    public void ClearItemsIcons()
    {
        for (int i = 0; i < itemsCollection.childCount; i++)
        {
            Destroy(itemsCollection.GetChild(i).gameObject);
        }
    }
    public void InstantiateItemsIcons()
    {
        ClearItemsIcons();
        for (int i = 0; i < container.itemsInContainer.Count; i++)
        {
            Sprite iconImage = container.itemsInContainer[i].itemData.icon;
            Vector3 instantiatePosition = new Vector3(gridStartPosition.position.x + container.itemsInContainer[i].coordianatesInContainer[0].x * nextCellDistance, gridStartPosition.position.y - container.itemsInContainer[i].coordianatesInContainer[0].y * nextCellDistance);
            GameObject iconSize = new GameObject();

            switch (container.itemsInContainer[i].itemData.size)
            {
                case ItemData.sizeInInventory.OneCell:
                    iconSize = iconSize1;
                    break;
                case ItemData.sizeInInventory.TwoCells:
                    iconSize = iconSize2;
                    break;
                case ItemData.sizeInInventory.ThreeCells:
                    iconSize = iconSize3;
                    break;
                case ItemData.sizeInInventory.FourCells:
                    iconSize = iconSize4;
                    break;
            }

            GameObject newIcon = Instantiate(iconSize, instantiatePosition, Quaternion.identity, itemsCollection);
            ItemIcon itemIcon = newIcon.GetComponent<ItemIcon>();
            itemIcon.item = container.itemsInContainer[i];
            itemIcon.container = container;
            itemIcon.grid = this;
            itemIcon.itemsCollector = itemsCollector;
            newIcon.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = iconImage;
        }
    }
    public Vector3 GetPointVector(Coordinates coordinates)
    {
       return new Vector3(gridStartPosition.position.x + coordinates.x * nextCellDistance, gridStartPosition.position.y - coordinates.y * nextCellDistance);
    }
}
