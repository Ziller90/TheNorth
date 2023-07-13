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
        RemoveSelection();
        selectedItemIcon = itemIcon;
        selectedItemSlot = GetItemIconSlot(itemIcon);
        selectedItemSlot.SetSlotSelection(true);
        selectedItemSlot.ShowItemShadow(itemIcon);
        selectedItemIconUpdated();
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
        selectedItemIconUpdated();
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
}
