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
    List<Slot> activeSlots = new List<Slot>();
    ItemIcon selectedItemIcon;
    Slot selectedItemSlot;

    public ItemIcon SelectedItemIcon => selectedItemIcon;
    public Slot SelectedItemSlot => selectedItemSlot;
    public Transform CommonIconsContainer => commonIconsContainer;

    public event Action selectedItemIconUpdated;

    void OnDisable()
    {
        selectedItemSlot = null;
        selectedItemIcon = null;
        RemoveSelection();
    }
    public void AddActiveSlot(Slot slot)
    {
        activeSlots.Add(slot);
    }

    public void RemoveActiveSlot(Slot slot)
    {
        if (activeSlots.Contains(slot))
            activeSlots.Remove(slot);
    }

    public Slot GetItemIconSlot(ItemIcon itemIcon)
    {
        return activeSlots.FirstOrDefault(i => i.ItemIcon == itemIcon);
    }

    public void SetSelectedIcon(ItemIcon itemIcon)
    {
        Debug.Log(itemIcon + "Set seleted");

        RemoveSelection();
        selectedItemIcon = itemIcon;
        selectedItemSlot = GetItemIconSlot(itemIcon);
        selectedItemSlot.SetSlotSelection(true);
        selectedItemSlot.ShowItemShadow(itemIcon);
        selectedItemIconUpdated?.Invoke();
    }
    public void RemoveSelection()
    {
        Debug.Log(selectedItemSlot + "removed selection");
        if (selectedItemSlot)
        {
            selectedItemSlot.SetSlotSelection(false);
            selectedItemSlot.HideItemShadow();
            selectedItemIcon = null;
            selectedItemSlot = null;
        }
        selectedItemIconUpdated?.Invoke();
    }

    public Slot GetSlotByPosition(Vector2 itemPosition)
    { 
        foreach (var slot in activeSlots)
        {
            if (slot.IsPositionInSlot(itemPosition))
                return slot;
        }
        return null;
    }

    public void MoveItemToSlot(ItemIcon itemIcon, Slot targetSlot)
    {
        var currentSlot = GetItemIconSlot(itemIcon);
        if (targetSlot == null || targetSlot == currentSlot)
        {
            currentSlot.StartCoroutine(currentSlot.SetIconInSlotPosition(itemIcon));
            SetSelectedIcon(itemIcon);
        }
        else if (targetSlot.ItemIcon == null)
        {
            currentSlot.RemoveIcon();
            targetSlot.InsertIcon(itemIcon, false);
            SetSelectedIcon(itemIcon);
        }
        else
        {
            var stack1 = currentSlot.ItemIcon.ItemStack;
            var stack2 = targetSlot.ItemIcon.ItemStack;

            if (ModelUtils.CanBeMerged(stack1, stack2))
            {
                MergeView(itemIcon, targetSlot.ItemIcon);
                ModelUtils.Merge(stack1, stack2);
            }
            else
                SwapItemIcons(currentSlot, targetSlot);
        }
    }
    public void MergeView(ItemIcon itemIcon1, ItemIcon itemIcon2)
    {
        var stack1 = itemIcon1.ItemStack;
        var stack2 = itemIcon2.ItemStack;
        var slot1 = GetItemIconSlot(itemIcon1);
        var slot2 = GetItemIconSlot(itemIcon2);
        int MaxStackSize = stack1.Item.MaxStackSize;

        if (stack1.ItemsNumber + stack2.ItemsNumber <= MaxStackSize)
        {
            SetSelectedIcon(slot2.ItemIcon);
            slot1.DestroyItemIcon();
        }
        else if (stack1.ItemsNumber + stack2.ItemsNumber >= MaxStackSize)
        {
            slot1.StartCoroutine(slot1.SetIconInSlotPosition(itemIcon1));
            SetSelectedIcon(slot2.ItemIcon);
        }
    }
    public void SwapItemIcons(Slot slot1, Slot slot2)
    {
        var itemIcon1 = slot1.ItemIcon;
        slot1.RemoveIcon();
        slot1.InsertIcon(slot2.ItemIcon, false);
        slot2.RemoveIcon();
        slot2.InsertIcon(itemIcon1, false);
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
