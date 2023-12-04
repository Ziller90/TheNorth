using System;
using System.Collections.Generic;
using UnityEngine;

public enum TransferResult
{
    Added = 0,
    Merged = 1,
    Swapped = 2,
    SlotIsBlocked = 3,
    NotSuitableItemStack = 4,
    NoFreeSpace = 5,
    CantSwap = 6,
    CurrentAndTargetAreSame = 7,
    Undefined = 7,
}

[Serializable]
public class AndItemTagList
{
    [SerializeField] List<ItemTag> and;
    public List<ItemTag> List => and;
}

[Serializable]
public class Slot
{
    [SerializeField] List<AndItemTagList> suitableItemTags;
    [SerializeReference, SubclassSelector] ItemStack itemStack = null;
    public ItemStack ItemStack => itemStack;
    public Sprite BlockImage => blockImage;
    public bool isEmpty => itemStack == null;
    public bool IsBlocked { get; set; }

    public delegate void SlotRemovedAction(ItemStack itemStack);
    public delegate void SetBlockAction(bool isBlocked, Sprite blockImage = null);

    public event SetBlockAction setBlock;

    public event Action inserted;
    public event SlotRemovedAction removed;

    Sprite blockImage;

    public void SetBlock(bool isBlocked, Sprite blockImage = null)
    {
        this.blockImage = blockImage;
        IsBlocked = isBlocked;
        setBlock?.Invoke(isBlocked, blockImage);
    }

    public bool TryAdd(ItemStack itemStack)
    {
        var result = TryAddWithResult(itemStack);
        return result == TransferResult.Added || result == TransferResult.Merged;
    }

    public TransferResult TryAddWithResult(ItemStack itemStack)
    {
        if (IsBlocked)
            return TransferResult.SlotIsBlocked;
        if (!IsSuitable(itemStack))
            return TransferResult.NotSuitableItemStack;

        if (CanBeMerged(itemStack))
        {
            Merge(itemStack);
            return TransferResult.Merged;
        }
        else if (isEmpty)
        {
            SetItemStack(itemStack);
            return TransferResult.Added;
        }
        else
            return TransferResult.NoFreeSpace;
    }

    public bool IsSuitable(ItemStack itemStack)
    {
        return ModelUtils.IsSuitableItemStack(itemStack, suitableItemTags);
    }

    public ItemStack Pop()
    {
        var itemStackTemp = itemStack;
        itemStack = null;
        removed?.Invoke(itemStackTemp);
        return itemStackTemp;
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

    void SetItemStack(ItemStack itemStack)
    {
        this.itemStack = itemStack;
        inserted?.Invoke();
    }

    void Merge(ItemStack itemStackToMerge)
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
