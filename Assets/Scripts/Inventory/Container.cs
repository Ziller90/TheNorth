using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Container : MonoBehaviour
{
    public List<Item> itemsInContainer;
    public int xSize;
    public int ySize;
    public bool[,] isFilledCell;

    void Start()
    {
        isFilledCell = new bool[ySize, xSize];
    }

    public void AddNewItem(Item item)
    {
        Coordinates newPosition;
        if (HasFreeCell(item, out newPosition))
        {
            SetFilled(newPosition, item);
            itemsInContainer.Add(item);
        }
        else
        {
            Debug.Log("No free space for this item");
        }
    }
    public void RemoveItem(Item item)
    {
        itemsInContainer.Remove(item);
    }
    public void SetFilled(Coordinates startPosition, Item item)
    {
        for (int o = 0; o < item.points.Length; o++)
        {
            isFilledCell[startPosition.y + item.points[o].y, startPosition.x + item.points[o].x] = true;
        }
        for (int i = 0; i < item.coordianatesInContainer.Length; i++)
        {
            item.coordianatesInContainer[i] = new Coordinates(startPosition.y + item.points[i].y, startPosition.x + item.points[i].x);
        }
    }
    public void SetEmpty(Item item)
    {
        for (int o = 0; o < item.coordianatesInContainer.Length; o++)
        {
            isFilledCell[item.coordianatesInContainer[o].y, item.coordianatesInContainer[o].x] = false;
        }
        for (int i = 0; i < item.coordianatesInContainer.Length; i++)
        {
            item.coordianatesInContainer[i] = new Coordinates(0,0);
        }
    }
    public bool CheckAllPoints(Coordinates start, Item item)
    {
        try
        {
            for (int o = 0; o < item.points.Length; o++)
            {
                if (isFilledCell[start.y + item.points[o].y, start.x + item.points[o].x] == true)
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception exeption)
        {
            return false;
        }
    }
    public bool HasFreeCell(Item item, out Coordinates freePoint)
    {
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Coordinates start = new Coordinates(i, j);
                if (CheckAllPoints(start, item))
                {
                    freePoint = new Coordinates(i, j);
                    return true;
                }
            }
        }
        freePoint = new Coordinates(0,0);
        return false;
    }
    public void DebugInventory()
    {
        string str = " ";
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                str += (" " + isFilledCell[i, j]);
            }
            str += "\n";
        }
        Debug.Log(str);
    }

}
