using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System;

public class ItemsViewManager : MonoBehaviour
{
    [SerializeField] Transform commonIconsContainer;
    [SerializeField] SlotView mainWeaponSlot;
    [SerializeField] SlotView secondaryWeaponSlot;

    List<SlotView> activeSlots = new List<SlotView>();

    ItemIcon selectedItemIcon;
    SlotView selectedItemSlot;

    public ItemIcon SelectedItemIcon => selectedItemIcon;
    public SlotView SelectedItemSlot => selectedItemSlot;
    public Transform CommonIconsContainer => commonIconsContainer;

    public event Action selectedItemIconUpdated;

    void OnDisable()
    {
        selectedItemSlot = null;
        selectedItemIcon = null;
        RemoveSelection();
    }
    public void AddActiveSlot(SlotView slot)
    {
        activeSlots.Add(slot);
    }

    public void RemoveActiveSlot(SlotView slot)
    {
        if (activeSlots.Contains(slot))
            activeSlots.Remove(slot);
    }

    public SlotView GetItemIconSlot(ItemIcon itemIcon)
    {
        return activeSlots.FirstOrDefault(i => i.ItemIcon == itemIcon);
    }

    public void SetSelectedIcon(ItemIcon itemIcon)
    {
        RemoveSelection();
        selectedItemIcon = itemIcon;
        selectedItemSlot = GetItemIconSlot(itemIcon);
        selectedItemSlot.SetSlotSelection(true);
        selectedItemSlot.ShowItemShadow(itemIcon);
        selectedItemIconUpdated?.Invoke();
    }

    public void RemoveSelection()
    {
        if (selectedItemSlot)
        {
            selectedItemSlot.SetSlotSelection(false);
            selectedItemSlot.HideItemShadow();
            selectedItemIcon = null;
            selectedItemSlot = null;
        }
        selectedItemIconUpdated?.Invoke();
    }

    public SlotView GetSlotByPosition(Vector2 itemPosition)
    { 
        foreach (var slot in activeSlots)
        {
            if (slot.IsPositionInSlot(itemPosition))
                return slot;
        }
        return null;
    }

    public void MoveItemToSlot(ItemIcon itemIcon, SlotView targetSlotView)
    {
        var currentSlotView = GetItemIconSlot(itemIcon);

        if (targetSlotView == null || targetSlotView == currentSlotView || !targetSlotView.IsSuitableSlotType(itemIcon.ItemStack.Item.SuitableSlots) || targetSlotView.IsBlocked)
        {
            currentSlotView.SetIconInSlotPosition();
            SetSelectedIcon(itemIcon);
            return;
        }

        if (targetSlotView.SlotType == SlotType.MainWeapon && itemIcon.ItemStack.Item.SuitableSlots == SlotType.TwoHanded)
        {
            if (secondaryWeaponSlot.ItemIcon != null)
            {
                var firstFreeSlot = activeSlots.FirstOrDefault(i => i.SlotType == SlotType.None && i.ItemIcon == null);

                if (firstFreeSlot)
                    MoveItemToSlot(secondaryWeaponSlot.ItemIcon, firstFreeSlot);
                else
                {
                    Debug.Log("Can't take two handed Weapon because there is no place in inventory");

                    SetSelectedIcon(itemIcon);
                    return;
                }
            }
        }

        if (targetSlotView.ItemIcon == null)
        {
            currentSlotView.PullOutIcon();
            targetSlotView.InsertIcon(itemIcon, false);
            SetSelectedIcon(itemIcon);
        }
        else
        {
            var stackToMerge = itemIcon.ItemStack;

            if (targetSlotView.Slot.CanBeMerged(stackToMerge))
            {
                MergeView(itemIcon, targetSlotView.ItemIcon);
                targetSlotView.Slot.Merge(stackToMerge);
            }
            else
                SwapItemIcons(currentSlotView, targetSlotView);
        }
    }

    public void MergeView(ItemIcon itemIcon1, ItemIcon itemIcon2)
    {
        var stack1 = itemIcon1.ItemStack;
        var stack2 = itemIcon2.ItemStack;
        var slotView1 = GetItemIconSlot(itemIcon1);
        var slotView2 = GetItemIconSlot(itemIcon2);
        int MaxStackSize = stack1.Item.MaxStackSize;

        if (stack1.ItemsNumber + stack2.ItemsNumber <= MaxStackSize)
        {
            SetSelectedIcon(slotView2.ItemIcon);
            slotView1.DestroyItemIcon();
        }
        else if (stack1.ItemsNumber + stack2.ItemsNumber >= MaxStackSize)
        {
            slotView1.SetIconInSlotPosition();
            SetSelectedIcon(slotView2.ItemIcon);
        }
    }

    public void SwapItemIcons(SlotView slot1, SlotView slot2)
    {
        var itemIcon1 = slot1.ItemIcon;
        slot1.PullOutIcon();
        slot1.InsertIcon(slot2.ItemIcon, false);
        slot2.PullOutIcon();
        slot2.InsertIcon(itemIcon1, false);

        SetSelectedIcon(slot2.ItemIcon);
    }

    public void DivideSelectedItem()
    {
        if (SelectedItemIcon.ItemStack.ItemsNumber == 1)
        {
            Debug.Log("Can't divide one item");
            return;
        }
    }
}
