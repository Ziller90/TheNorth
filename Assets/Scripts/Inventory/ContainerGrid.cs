using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGrid : MonoBehaviour
{
    public Transform gridStartPosition;
    public float iconSideLength;

    [HideInInspector] public Vector3 leftTopCorner;
    [HideInInspector] public Vector3 rightBottomCorner;

    [SerializeField] Container container;
    [SerializeField] ItemsCollector itemsCollector;
    [SerializeField] GameObject iconTemplate;
    [SerializeField] float gridRangeFactor;
    [SerializeField] Transform gridNextPosition;
    [SerializeField] Transform itemsCollection;

    private void Awake()
    {
        iconSideLength = Vector3.Distance(gridStartPosition.position, gridNextPosition.position);
    }
    void Start()
    {
        leftTopCorner = new Vector3(gridStartPosition.position.x - iconSideLength * gridRangeFactor, gridStartPosition.position.y + iconSideLength * gridRangeFactor, 0);
        Vector3 lastPointVector = GetPointVector(new Coordinates(container.ySize - 1, container.xSize - 1));
        rightBottomCorner = new Vector3(lastPointVector.x + iconSideLength * gridRangeFactor, lastPointVector.y - iconSideLength * gridRangeFactor, 0);
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
    Vector3 GetCorrectionVector(int xIconSize, int yIconSize)
    {
        return new Vector3((xIconSize - 1) * iconSideLength * 0.5f, (yIconSize - 1) * iconSideLength * 0.5f);
    }
    public void InstantiateItemsIcons()
    {
        ClearItemsIcons();
        for (int i = 0; i < container.itemsInContainer.Count; i++)
        {
            Sprite iconImage = container.itemsInContainer[i].itemData.icon;
            Vector3 instantiatePosition = new Vector3(gridStartPosition.position.x + container.itemsInContainer[i].coordianatesInContainer[0].x * iconSideLength, gridStartPosition.position.y - container.itemsInContainer[i].coordianatesInContainer[0].y * iconSideLength);
            GameObject newIcon = Instantiate(iconTemplate, instantiatePosition, Quaternion.identity, itemsCollection);
            ItemIcon itemIcon = newIcon.GetComponent<ItemIcon>();
            Vector3 correctionVector = new Vector3();
            switch (container.itemsInContainer[i].itemData.size)
            {
                case ItemData.sizeInInventory.OneCell:
                    itemIcon.SetNewIconTemplateSize(1, 1, iconSideLength);
                    correctionVector = GetCorrectionVector(1, 1);
                    break;
                case ItemData.sizeInInventory.TwoCells:
                    itemIcon.SetNewIconTemplateSize(1, 2, iconSideLength);
                    correctionVector = GetCorrectionVector(1, 2);
                    break;
                case ItemData.sizeInInventory.ThreeCells:
                    itemIcon.SetNewIconTemplateSize(1, 3, iconSideLength);
                    correctionVector = GetCorrectionVector(1, 3);
                    break;
                case ItemData.sizeInInventory.FourCells:
                    itemIcon.SetNewIconTemplateSize(2, 2, iconSideLength);
                    correctionVector = GetCorrectionVector(2, 2);
                    break;
            }
            itemIcon.item = container.itemsInContainer[i];
            itemIcon.container = container;
            itemIcon.grid = this;
            itemIcon.itemsCollector = itemsCollector;
            newIcon.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = iconImage;
            newIcon.transform.position += correctionVector;
        }
    }
    public Vector3 GetPointVector(Coordinates coordinates)
    {
       return new Vector3(gridStartPosition.position.x + coordinates.x * iconSideLength, gridStartPosition.position.y - coordinates.y * iconSideLength);
    }
}
