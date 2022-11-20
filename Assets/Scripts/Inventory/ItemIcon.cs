using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Item item;
    [HideInInspector] public Container container;
    [HideInInspector] public ItemsCollector itemsCollector;
    [HideInInspector] public ContainerGrid grid;
    [HideInInspector] public DescriptionShower descriptionShower;
    [SerializeField] RectTransform background;
    [SerializeField] RectTransform iconImage;
    [SerializeField] RectTransform icon;
    

    Coordinates oldStartCoordinates;
    Vector3 oldPosition;
    Vector3 offset;
    void Start()
    {
        Debug.Log(item.itemData.description);
        switch (item.itemData.size)
        {
            case ItemData.sizeInInventory.OneCell:
                offset = Vector3.zero;
                break;
            case ItemData.sizeInInventory.TwoCells:
                offset = new Vector3(0, -grid.squareSideLength / 2, 0);
                break;
            case ItemData.sizeInInventory.ThreeCells:
                offset = new Vector3(0, -grid.squareSideLength, 0);
                break;
            case ItemData.sizeInInventory.FourCells:
                offset = new Vector3(grid.squareSideLength / 2, -grid.squareSideLength / 2, 0);
                break;
        }
    }
    public void SetNewIconTemplate(int x, int y, float squareSideLength, float iconSizeModificator)
    {
        background.sizeDelta = new Vector2(squareSideLength * x, squareSideLength * y);
        iconImage.sizeDelta = new Vector2(squareSideLength * x * iconSizeModificator, squareSideLength * y * iconSizeModificator);
        icon.sizeDelta = new Vector2(squareSideLength * x, squareSideLength * y);

        Vector3 correctionVector = new Vector3(squareSideLength * 0.5f * (x - 1), -(squareSideLength * 0.5f * (y - 1)), 0);
        background.position += correctionVector;
        iconImage.position += correctionVector;
    }
    public Coordinates GetCoordinates(Vector3 pointerPosition)
    {
        Coordinates coordinates = new Coordinates(0, 0);

        Vector3 pointerRelativeToGrid = pointerPosition - grid.gridStartPosition.position;
        pointerRelativeToGrid.y = -pointerRelativeToGrid.y;

        coordinates.x = Mathf.RoundToInt(pointerRelativeToGrid.x /grid.squareSideLength);
        coordinates.y = Mathf.RoundToInt(pointerRelativeToGrid.y / grid.squareSideLength);
        return coordinates;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        oldStartCoordinates = item.coordianatesInContainer[0];
        oldPosition = gameObject.transform.position;
        container.SetEmpty(item);
        descriptionShower.ShowDescription(this);
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Vector3 pointerPosition = new Vector3(pointerEventData.position.x, pointerEventData.position.y, 0);
        Vector3 itemStartPointPosition = pointerPosition - offset;

        if (CheckInRangeOfGrid(itemStartPointPosition))
        {
            Coordinates newStartPoint = GetCoordinates(itemStartPointPosition);
            if (container.CheckAllPoints(newStartPoint, item))
            {
                SetNewItemPosition(newStartPoint);
            }
            else
            {
                ReturnToOldPosition();
            }
        }
        else
        {
            ReturnToOldPosition();
        }
    }
    public void SetNewItemPosition(Coordinates newStartPoint)
    {
        container.SetFilled(newStartPoint, item);
        gameObject.transform.position = grid.GetPointVector(newStartPoint);
    }
    public void ReturnToOldPosition()
    {
        container.SetFilled(oldStartCoordinates, item);
        gameObject.transform.position = oldPosition;
    }
    public bool CheckInRangeOfGrid(Vector3 position)
    {
        if (position.x > grid.leftTopCorner.x && 
            position.x < grid.rightBottomCorner.x && 
            position.y < grid.leftTopCorner.y &&
            position.y > grid.rightBottomCorner.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        gameObject.transform.position = new Vector3(pointerEventData.position.x, pointerEventData.position.y,0) - offset;
    }
}
