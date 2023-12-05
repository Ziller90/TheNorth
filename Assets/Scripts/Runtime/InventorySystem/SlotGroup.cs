using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SlotGroup
{
    [SerializeField] List<AndItemTagList> suitableItemTags;
    [SerializeField] Slot[] slots;
    public Slot[] Slots => slots;

    public bool CanAdd(ItemStack itemStack)
    {
        if (!IsSuitableItemStackForGroup(itemStack))
            return false;
        if (GetFirstEmtySlotInGroupIndex() == -1)
            return false;

        return true;
    }

    public bool TryAddOrMerge(ItemStack itemStack)
    {
        var result = TryAddOrMergeAndReturnResult(itemStack);
        return result == TransferResult.Added || result == TransferResult.Merged;
    }

    public TransferResult TryAddOrMergeAndReturnResult(ItemStack itemStack)
    {
        if (!IsSuitableItemStackForGroup(itemStack))
            return TransferResult.NotSuitableItemStack;

        bool merged = TryMergeToSlotGroup(itemStack);
        if (merged)
            return TransferResult.Merged;

        bool added = TryAddItemStackToSlotGroup(itemStack);
        if (added)
            return TransferResult.Added;

        return TransferResult.NoFreeSpace;
    }

    int GetFirstEmtySlotInGroupIndex()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isEmpty)
                return i;
        }
        return -1;
    }

    public bool TryAddItemStackToSlotGroup(ItemStack newStack)
    {
        int freeStackIndex = GetFirstEmtySlotInGroupIndex();
        if (freeStackIndex == -1)
            return false;

        slots[freeStackIndex].TryAdd(newStack);
        return true;
    }

    bool TryMergeToSlotGroup(ItemStack newStack)
    {
        if (newStack.Item.MaxStackSize != 1)
        {
            foreach (var slot in slots)
            {
                if (slot.CanBeMerged(newStack))
                {
                    slot.TryAdd(newStack);
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsSuitableItemStackForGroup(ItemStack itemStack)
    {
        return ModelUtils.IsSuitableItemStack(itemStack, suitableItemTags);
    }

    public bool isSlotInSlotGroup(Slot slotForCheck)
    {
        foreach(var slot in slots)
        {
            if (slot == slotForCheck)
                return true;
        }
        return false;
    }
}
