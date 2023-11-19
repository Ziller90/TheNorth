using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class ItemStack
{
    [SerializeField] Item itemPrefab;
    [SerializeField] int itemsNumber;

    public Item Item => itemPrefab;
    public Action itemsNumberUpdatedEvent;
    public Action deletedEvent;

    public int ItemsNumber
    {
        get
        {
            return itemsNumber;
        }
        set
        {
            itemsNumber = value;
            itemsNumberUpdatedEvent?.Invoke();
            if (itemsNumber <= 0)
            {
                deletedEvent?.Invoke();
                itemPrefab = null;
            }
        }
    }

    public ItemStack(Item prefab, int quantity)
    {
        itemPrefab = prefab;
        itemsNumber = quantity;
    }

    public ItemStack()
    {
        itemPrefab = null;
        itemsNumber = 0;
    }
}
