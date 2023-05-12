using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGrid : MonoBehaviour
{
    public Transform startPosition;
    public Transform nextPosition;

    public float distanceBeetweenSquaresCenters;
    public float distanceBeetweenSquares;
    public float squareSideLength;

    public float iconSizeModificator;

    [HideInInspector] public Vector3 leftTopCorner;
    [HideInInspector] public Vector3 rightBottomCorner;

    [SerializeField] Container container;
    [SerializeField] DescriptionShower descriptionShower;
    [SerializeField] GameObject iconTemplate;
    [SerializeField] float gridRangeFactor;
    [SerializeField] Transform itemsCollection;

    ItemsCollector itemsCollector;

    void SetLinksToPlayerComponents()
    {
        itemsCollector = Links.instance.playerCharacter.GetComponentInChildren<ItemsCollector>();
        container = Links.instance.playerCharacter.GetComponentInChildren<Container>();
    }
    private void OnEnable()
    {
        SetLinksToPlayerComponents();
        squareSideLength = startPosition.gameObject.GetComponent<RectTransform>().rect.width;
        distanceBeetweenSquaresCenters = Vector3.Distance(startPosition.position, nextPosition.position);
        distanceBeetweenSquares = distanceBeetweenSquaresCenters - squareSideLength;

        leftTopCorner = new Vector3(startPosition.position.x - distanceBeetweenSquaresCenters * gridRangeFactor, startPosition.position.y + distanceBeetweenSquaresCenters * gridRangeFactor, 0);
        Vector3 lastPointVector = GetPointVector(new Coordinates(container.ySize - 1, container.xSize - 1));
        rightBottomCorner = new Vector3(lastPointVector.x + distanceBeetweenSquaresCenters * gridRangeFactor, lastPointVector.y - distanceBeetweenSquaresCenters * gridRangeFactor, 0);
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
        return new Vector3((xIconSize - 1) * distanceBeetweenSquaresCenters * 0.5f, (yIconSize - 1) * distanceBeetweenSquaresCenters * 0.5f);
    }
    public void InstantiateItemsIcons()
    {
        ClearItemsIcons();
        for (int i = 0; i < container.itemsInContainer.Count; i++)
        {
            Sprite iconImage = container.itemsInContainer[i].ItemData.icon;
            Vector3 instantiatePosition = new Vector3(startPosition.position.x + container.itemsInContainer[i].CoordianatesInContainer[0].x * distanceBeetweenSquaresCenters, startPosition.position.y - container.itemsInContainer[i].CoordianatesInContainer[0].y * distanceBeetweenSquaresCenters);
            GameObject newIcon = Instantiate(iconTemplate, instantiatePosition, Quaternion.identity, itemsCollection);
            ItemIcon itemIcon = newIcon.GetComponent<ItemIcon>();

            switch (container.itemsInContainer[i].ItemData.size)
            {
                case ItemData.sizeInInventory.OneCell:
                    itemIcon.SetNewIconTemplate(1, 1, squareSideLength, distanceBeetweenSquares, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.TwoCells:
                    itemIcon.SetNewIconTemplate(1, 2, squareSideLength, distanceBeetweenSquares, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.ThreeCells:
                    itemIcon.SetNewIconTemplate(1, 3, squareSideLength, distanceBeetweenSquares, iconSizeModificator);
                    break;
                case ItemData.sizeInInventory.FourCells:
                    itemIcon.SetNewIconTemplate(2, 2, squareSideLength, distanceBeetweenSquares, iconSizeModificator);
                    break;
            }
            itemIcon.item = container.itemsInContainer[i];
            itemIcon.container = container;
            itemIcon.grid = this;
            itemIcon.descriptionShower = descriptionShower;
            itemIcon.itemsCollector = itemsCollector;
            newIcon.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = iconImage;
        }
        Debug.Log("squareSideLength - " + distanceBeetweenSquaresCenters + " leftTopCorner - " + leftTopCorner + " rightBottomCorner - " + rightBottomCorner + " gridStartPosition - " + startPosition.localPosition);
    }
    public Vector3 GetPointVector(Coordinates coordinates)
    {
       return new Vector3(startPosition.position.x + coordinates.x * distanceBeetweenSquaresCenters, startPosition.position.y - coordinates.y * distanceBeetweenSquaresCenters);
    }
}
