using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Container : MonoBehaviour
{
    [SerializeField] int maxItemsNumber;
    int itemsNumber = 0;
    [SerializeField] Item[] itemsInContainer;
    public int MaxItemsNumber => maxItemsNumber;
    public bool HasFreeSpace => GetFreeContainerIndex() == -1 ? false : true;

    public void Start()
    {
        itemsInContainer = new Item[maxItemsNumber];
    }
    public void AddNewItem(Item item)
    {
        AddItemInIndex(item, GetFreeContainerIndex());
    }
    public void AddItemInIndex(Item item, int index)
    {
        itemsInContainer[index] = item;
        itemsNumber++;
    }
    int GetFreeContainerIndex()
    {
        for (int i = 0; i < maxItemsNumber; i++)
        {
            if (itemsInContainer[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public Item GetItem(int index)
    {
        return itemsInContainer[index];
    }
    public void RemoveItem(Item item, int index)
    {
        itemsInContainer[index] = null;
        itemsNumber--;
    }
}
