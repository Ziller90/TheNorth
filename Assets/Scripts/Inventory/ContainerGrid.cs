using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGrid : MonoBehaviour
{
    public Container container;
    public ItemsCollector itemsCollector;
    public GameObject IconSize1;
    public GameObject IconSize2;
    public GameObject IconSize3;
    public GameObject IconSize4;
    public Transform canvasTransform;

    public Vector3 LeftTopCorner;
    public Vector3 RightBottomCorner;
    public float gridRangeFactor;

    public Transform gridStartPosition;
    public Transform gridNextPosition;
    public Transform ItemsCollection;
    public float nextCellDistance;

    public Transform TrashCan;
    public float trashCanRange;

    private void Awake()
    {
        nextCellDistance = Vector3.Distance(gridStartPosition.position, gridNextPosition.position);
        Debug.Log(nextCellDistance);
    }
    void Start()
    {
        LeftTopCorner = new Vector3(gridStartPosition.position.x - nextCellDistance * gridRangeFactor, gridStartPosition.position.y + nextCellDistance * gridRangeFactor, 0);
        Vector3 LastPointVector = GetPointVector(new Coordinates(container.ySize - 1, container.xSize - 1));
        RightBottomCorner = new Vector3(LastPointVector.x + nextCellDistance * gridRangeFactor, LastPointVector.y - nextCellDistance * gridRangeFactor, 0);
    }
    private void OnEnable()
    {
        InstantiateItemsIcons();
    }
    public void ClearItemsIcons()
    {
        for (int i = 0; i < ItemsCollection.childCount; i++)
        {
            Destroy(ItemsCollection.GetChild(i).gameObject);
        }
    }
    public void InstantiateItemsIcons()
    {
        Debug.Log("ContentRefreshed");
        ClearItemsIcons();
        for (int i = 0; i < container.itemsInContainer.Count; i++)
        {
            Sprite iconImage = container.itemsInContainer[i].itemData.icon;
            Vector3 instantiatePosition = new Vector3(gridStartPosition.position.x + container.itemsInContainer[i].coordianatesInContainer[0].x * nextCellDistance, gridStartPosition.position.y - container.itemsInContainer[i].coordianatesInContainer[0].y * nextCellDistance);
            GameObject iconSize = new GameObject();

            switch (container.itemsInContainer[i].itemData.size)
            {
                case ItemData.sizeInInventory.oneCell:
                    iconSize = IconSize1;
                    break;
                case ItemData.sizeInInventory.twoCells:
                    iconSize = IconSize2;
                    break;
                case ItemData.sizeInInventory.threeCells:
                    iconSize = IconSize3;
                    break;
                case ItemData.sizeInInventory.fourCells:
                    iconSize = IconSize4;
                    break;
            }

            GameObject newIcon = Instantiate(iconSize, instantiatePosition, Quaternion.identity, ItemsCollection);
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
