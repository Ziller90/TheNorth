using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGrid : MonoBehaviour
{
    public Transform gridStartPosition;
    public float squareSideLength;
    public float iconSizeModificator;


    [HideInInspector] public Vector3 leftTopCorner;
    [HideInInspector] public Vector3 rightBottomCorner;

    [SerializeField] Container container;
    [SerializeField] ItemsCollector itemsCollector;
    [SerializeField] DescriptionShower descriptionShower;
    [SerializeField] GameObject iconTemplate;
    [SerializeField] float gridRangeFactor;
    [SerializeField] Transform gridNextPosition;
    [SerializeField] Transform itemsCollection;

    private void Awake()
    {
        squareSideLength = Vector3.Distance(gridStartPosition.position, gridNextPosition.position);
    }
    void Start()
    {
        leftTopCorner = new Vector3(gridStartPosition.position.x - squareSideLength * gridRangeFactor, gridStartPosition.position.y + squareSideLength * gridRangeFactor, 0);
        Vector3 lastPointVector = GetPointVector(new Coordinates(container.ySize - 1, container.xSize - 1));
        rightBottomCorner = new Vector3(lastPointVector.x + squareSideLength * gridRangeFactor, lastPointVector.y - squareSideLength * gridRangeFactor, 0);
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
        return new Vector3((xIconSize - 1) * squareSideLength * 0.5f, (yIconSize - 1) * squareSideLength * 0.5f);
    }
    public void InstantiateItemsIcons()
    {
        ClearItemsIcons();
        for (int i = 0; i < container.itemsInContainer.Count; i++)
        {
            Sprite iconImage = container.itemsInContainer[i].itemData.icon;
            Vector3 instantiatePosition = new Vector3(gridStartPosition.position.x + container.itemsInContainer[i].coordianatesInContainer[0].x * squareSideLength, gridStartPosition.position.y - container.itemsInContainer[i].coordianatesInContainer[0].y * squareSideLength);
            GameObject newIcon = Instantiate(iconTemplate, instantiatePosition, Quaternion.identity, itemsCollection);
            ItemIcon itemIcon = newIcon.GetComponent<ItemIcon>();

            switch (container.itemsInContainer[i].itemData.size)
            {
                case ItemData.sizeInInventory.OneCell:
                    itemIcon.SetNewIconTemplate(1, 1, squareSideLength, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.TwoCells:
                    itemIcon.SetNewIconTemplate(1, 2, squareSideLength, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.ThreeCells:
                    itemIcon.SetNewIconTemplate(1, 3, squareSideLength, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.FourCells:
                    itemIcon.SetNewIconTemplate(2, 2, squareSideLength, iconSizeModificator);
                    break;
            }
            itemIcon.item = container.itemsInContainer[i];
            itemIcon.container = container;
            itemIcon.grid = this;
            itemIcon.descriptionShower = descriptionShower;
            itemIcon.itemsCollector = itemsCollector;
            newIcon.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = iconImage;
        }
    }
    public Vector3 GetPointVector(Coordinates coordinates)
    {
       return new Vector3(gridStartPosition.position.x + coordinates.x * squareSideLength, gridStartPosition.position.y - coordinates.y * squareSideLength);
    }
}
