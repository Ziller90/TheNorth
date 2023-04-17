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
        SetEmpty(item);
    }
    public void SetFilled(Coordinates startPosition, Item item)
    {
        foreach(Coordinates point in item.points)
        {
            isFilledCell[startPosition.y + point.y, startPosition.x + point.x] = true;
        }
        for (int i = 0; i < item.coordianatesInContainer.Length; i++)
        {
            item.coordianatesInContainer[i] = new Coordinates(startPosition.y + item.points[i].y, startPosition.x + item.points[i].x);
        }
    }
    public void SetEmpty(Item item)
    {
        foreach (Coordinates point in item.coordianatesInContainer)
        {
            isFilledCell[point.y, point.x] = false;
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
            foreach (Coordinates point in item.points)
            {
                if (isFilledCell[start.y + point.y, start.x + point.x] == true)
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
    }
}
