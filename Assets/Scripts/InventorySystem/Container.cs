using UnityEngine;
using System.Linq;
using System.Collections.Generic;
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

    public ItemStack(Item itemPrefab, int quantity)
    {
        this.itemPrefab = itemPrefab;
        this.itemsNumber = quantity;
    }
}
public class Container : MonoBehaviour
{
    [SerializeField] ItemStack[] itemsStackInContainer;
    public ItemStack[] ItemsStacksInContainer => itemsStackInContainer;
    public int MaxItemStacksNumber => itemsStackInContainer.Length;
    public ItemStack GetItemStack(int index) => itemsStackInContainer[index];
    public void RemoveItemStackAtIndex(int index) => itemsStackInContainer[index] = new ItemStack(null, 0);
    public bool Contains(ItemStack itemStack) => itemsStackInContainer.Contains(itemStack);
    public void AddItemsStackInIndex(ItemStack itemStack, int index) => itemsStackInContainer[index] = itemStack;

    public void RemoveItemsStack(ItemStack itemStack)
    {
        for (int i = 0; i < MaxItemStacksNumber; i++)
        {
            if (itemsStackInContainer[i] == itemStack)
                RemoveItemStackAtIndex(i);
        }
    }
}
