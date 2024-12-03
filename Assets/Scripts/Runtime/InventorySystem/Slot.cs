using SiegeUp.Core;
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
    ItemIsNull = 8,
    Undefined = 9,
}

[Serializable]
public class AndItemTagList
{
    [AutoSerialize(1), SerializeField] List<ItemTag> and;
    public List<ItemTag> List => and;
}

[Serializable]
public class Slot
{
    [AutoSerialize(1), SerializeField] List<AndItemTagList> suitableItemTags;
    [AutoSerialize(2), SerializeReference, SubclassSelector] ItemStack itemStack = null;

    public ItemStack ItemStack => itemStack;
    public Sprite BlockSprite => blockSprite;
    public bool IsEmpty => itemStack == null || itemStack.Item == null;
    public bool IsBlocked { get; set; }

    public delegate void SlotRemovedAction(ItemStack itemStack);
    public delegate void SetBlockAction(bool isBlocked, Sprite blockSprite = null);

    public event SetBlockAction blockStateUpdated;

    public event Action inserted;
    public event SlotRemovedAction removed;

    Sprite blockSprite;



    public void SubscribeItemsNumberUpdateEvent()
    {
        if (itemStack != null)
        {
            itemStack.itemsNumberUpdatedEvent -= RemoveItemStackOnRunOut;
            itemStack.itemsNumberUpdatedEvent += RemoveItemStackOnRunOut;
        }
    }

    public void UnSubscribeItemsNumberUpdateEvent()
    {
        if (itemStack != null)
            itemStack.itemsNumberUpdatedEvent -= RemoveItemStackOnRunOut;
    }

    void RemoveItemStackOnRunOut()
    {
        if (itemStack.ItemsNumber == 0)
            Pop();
    }

    public void SetBlock(bool isBlocked, Sprite blockSprite = null)
    {
        this.blockSprite = blockSprite;
        IsBlocked = isBlocked;
        blockStateUpdated?.Invoke(isBlocked, blockSprite);
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
        else if (IsEmpty)
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
        UnSubscribeItemsNumberUpdateEvent();
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
        SubscribeItemsNumberUpdateEvent();
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
