using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using SiegeUp.Core;

[Serializable]
public class ItemStack
{
    [AutoSerialize(1), SerializeField] PrefabRef itemPrefab;
    [AutoSerialize(2), SerializeField] int itemsNumber;

    public PrefabRef ItemPrefab => itemPrefab;
    public Item Item => Game.PrefabManager.GetPrefab(itemPrefab).GetComponent<Item>();
    public Action itemsNumberUpdatedEvent;

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
                itemPrefab = null;
            }
        }
    }

    public ItemStack(PrefabRef itemPrefab, int quantity)
    {
        this.itemPrefab = itemPrefab;
        itemsNumber = quantity;
    }

    public ItemStack()
    {
        itemPrefab = null;
        itemsNumber = 0;    
    }
}
