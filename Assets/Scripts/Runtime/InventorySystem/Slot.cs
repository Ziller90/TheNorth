using System;
using UnityEngine;

[Serializable]
public class Slot
{
    [SerializeReference, SubclassSelector] ItemStack itemStack = null;
    public ItemStack ItemStack => itemStack;
    public bool isEmpty => itemStack == null;

    public void SetItemStack(ItemStack itemStack)
    {
        this.itemStack = itemStack;
    }

    public void SetEmpty()
    {
        itemStack = null;
    }

    public bool CanBeMerged(ItemStack itemStackToMerge)
    {
        if (itemStack != null 
            && itemStack.Item == itemStackToMerge.Item
            && itemStack.Item.MaxStackSize != 1
            && itemStackToMerge.Item.MaxStackSize != 1
            && itemStack.Item.MaxStackSize != itemStack.ItemsNumber
            && itemStackToMerge.Item.MaxStackSize != itemStackToMerge.ItemsNumber)
        {
            return true;
        }

        return false;
    }

    public void Merge(ItemStack itemStackToMerge)
    {
        int MaxStackSize = itemStack.Item.MaxStackSize;
        if (itemStack.ItemsNumber + itemStackToMerge.ItemsNumber <= MaxStackSize)
        {
            itemStack.ItemsNumber = itemStack.ItemsNumber + itemStackToMerge.ItemsNumber;
            itemStackToMerge.ItemsNumber = 0;
        }
        else if (itemStack.ItemsNumber + itemStackToMerge.ItemsNumber >= MaxStackSize)
        {
            int deficient = MaxStackSize - itemStack.ItemsNumber;
            itemStack.ItemsNumber = MaxStackSize;
            itemStackToMerge.ItemsNumber -= deficient;
        }
    }
}
