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
    [SerializeField] ItemData itemData;
    [SerializeField] Coordinates[] coordianatesInContainer;
    [SerializeField] Coordinates[] points;
    [SerializeField] bool inInventory;

    public ItemData ItemData => itemData;
    public Coordinates[] CoordianatesInContainer => coordianatesInContainer;
    public Coordinates[] Points => points;
    public bool InInventory { get => inInventory; set => inInventory = value; }

    void OnEnable()
    {
        if (!inInventory)
            Links.instance.globalLists.AddToItemsOnLocation(gameObject.transform);
    }
    void OnDisable()
    {
        if (!inInventory)
            Links.instance.globalLists.RemoveFromItemsOnLocation(gameObject.transform);
    }
    private void Start()
    {
        switch (itemData.size)
        {
            case ItemData.sizeInInventory.OneCell:
                points = new Coordinates[1];
                points[0] = new Coordinates(0, 0);
                break;
            case ItemData.sizeInInventory.TwoCells:
                points = new Coordinates[2];
                points[0] = new Coordinates(0, 0);
                points[1] = new Coordinates(1, 0);
                break;
            case ItemData.sizeInInventory.ThreeCells:
                points = new Coordinates[3];
                points[0] = new Coordinates(0, 0);
                points[1] = new Coordinates(1, 0);
                points[2] = new Coordinates(2, 0);
                break;
            case ItemData.sizeInInventory.FourCells:
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
