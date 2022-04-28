using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Coordinates
{
    public int x;
    public int y;
    public Coordinates(int y, int x)
    {
        this.x = x;
        this.y = y;
    }
}
public class Item : MonoBehaviour
{
    public ItemData itemData;
    public Coordinates[] coordianatesInContainer; 
    public Coordinates[] points;

    void Start()
    {
        LinksContainer.instance.globalLists.AddToItemsOnLocation(gameObject.transform);

        switch (itemData.size)
        {
            case ItemData.sizeInInventory.oneCell:
                points = new Coordinates[1];
                points[0] = new Coordinates(0, 0);
                break;
            case ItemData.sizeInInventory.twoCells:
                points = new Coordinates[2];
                points[0] = new Coordinates(0, 0);
                points[1] = new Coordinates(1, 0);
                break;
            case ItemData.sizeInInventory.threeCells:
                points = new Coordinates[3];
                points[0] = new Coordinates(0, 0);
                points[1] = new Coordinates(1, 0);
                points[2] = new Coordinates(2, 0);
                break;
            case ItemData.sizeInInventory.fourCells:
                points = new Coordinates[4];
                points[0] = new Coordinates(0, 0);
                points[1] = new Coordinates(1, 0);
                points[2] = new Coordinates(0, 1);
                points[3] = new Coordinates(1, 1);
                break;
        }
        coordianatesInContainer = new Coordinates[points.Length];
    }
}
