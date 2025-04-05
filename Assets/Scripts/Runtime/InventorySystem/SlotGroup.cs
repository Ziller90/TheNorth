using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SiegeUp.Core;
using System.Linq;

[Serializable]
public class SlotGroup
{
    [AutoSerialize(1), SerializeField] List<AndItemTagList> suitableItemTags = new();
    [AutoSerialize(2), SerializeField] List<Slot> slots = new();

    public List<Slot> Slots => slots;
    public bool IsEmpty => slots.All(i => i.IsEmpty);

    public void InitializeSlotsGroup(int slotsNumber)
    {
        slots = new List<Slot>();
        for (int i = 0; i < slotsNumber; i++)
            slots.Add(new Slot());
    }

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
        if (itemStack == null || itemStack.Item == null)
            return TransferResult.ItemIsNull;

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
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
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
