using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    public Item item;
    public Container container;
    public ItemsCollector itemsCollector;
    public ContainerGrid grid;
    public Coordinates oldStartCoordinates;
    public Vector3 oldPosition;
    Vector3 offset;
    void Start()
    {
        switch (item.itemData.size)
        {
            case ItemData.sizeInInventory.OneCell:
                offset = Vector3.zero;
                break;
            case ItemData.sizeInInventory.TwoCells:
                offset = new Vector3(0, -grid.nextCellDistance / 2, 0);
                break;
            case ItemData.sizeInInventory.ThreeCells:
                offset = new Vector3(0, -grid.nextCellDistance, 0);
                break;
            case ItemData.sizeInInventory.FourCells:
                offset = new Vector3(grid.nextCellDistance / 2, -grid.nextCellDistance / 2, 0);
                break;
        }
    }
    public Coordinates GetCoordinates(Vector3 pointerPosition)
    {
        Coordinates coordinates = new Coordinates(0, 0);

        Vector3 pointerRelativeToGrid = pointerPosition - grid.gridStartPosition.position;
        pointerRelativeToGrid.y = -pointerRelativeToGrid.y;

        coordinates.x = Mathf.RoundToInt(pointerRelativeToGrid.x /grid.nextCellDistance);
        coordinates.y = Mathf.RoundToInt(pointerRelativeToGrid.y / grid.nextCellDistance);
        return coordinates;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Coordinates in container " + item.coordianatesInContainer[0].y + " " + item.coordianatesInContainer[0].x);
        Debug.Log("Old coordinates in container " + oldStartCoordinates.y + " " + oldStartCoordinates.x);
        oldStartCoordinates = item.coordianatesInContainer[0];
        oldPosition = gameObject.transform.position;
        container.SetEmpty(item);
        Debug.Log("Coordinates in container " + item.coordianatesInContainer[0].y + " " + item.coordianatesInContainer[0].x);
        Debug.Log("Old coordinates in container " + oldStartCoordinates.y + " " + oldStartCoordinates.x);
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Vector3 pointerPosition = new Vector3(pointerEventData.position.x, pointerEventData.position.y, 0);
        Vector3 itemStartPointPosition = pointerPosition - offset;

        if (Vector3.Distance(pointerPosition, grid.trashCan.position) < grid.trashCanRange)
        {
            itemsCollector.Drop(item);
            Destroy(gameObject);
        }
        else
        {
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
        Debug.Log("Coordinates in container " + item.coordianatesInContainer[0].y + " " + item.coordianatesInContainer[0].x);
        Debug.Log("Old coordinates in container " + oldStartCoordinates.y + " " + oldStartCoordinates.x);
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
