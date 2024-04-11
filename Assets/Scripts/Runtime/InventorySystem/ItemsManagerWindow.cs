using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemsManagerWindow : MonoBehaviour
{
    // used to contain all slotViews that are active on UI at the moment
    public class ActiveSlot
    {
        public SlotView slotView;
        public ContainerBase container;
    }

    [SerializeField] Transform commonIconsContainer;

    List<ActiveSlot> activeSlots = new List<ActiveSlot>();

    SlotView selectedItemSlot;

    public ItemIcon SelectedItemIcon => selectedItemSlot ? selectedItemSlot.ItemIcon : null;
    public SlotView SelectedItemSlot => selectedItemSlot;
    public Transform CommonIconsContainer => commonIconsContainer;

    public event Action selectedSlotUpdated;

    void OnDisable()
    {
        RemoveSelection();
    }

    public void AddActiveSlot(ContainerBase container, SlotView slotView) => activeSlots.Add(new ActiveSlot {container = container, slotView = slotView } );
    public void RemoveActiveSlot(SlotView slotView) => activeSlots.RemoveAll(i => i.slotView == slotView);
    public ActiveSlot GetActiveSlotByItemIcon(ItemIcon itemIcon) => activeSlots.FirstOrDefault(i => i.slotView.ItemIcon == itemIcon);
    public void Clear() => activeSlots.Clear();

    public void SetSelectedSlotView(SlotView newSelectedSlotView)
    {
        RemoveSelection();
        selectedItemSlot = newSelectedSlotView;
        selectedItemSlot.SetSlotSelection(true);
        selectedSlotUpdated?.Invoke();
    }

    public void RemoveSelection()
    {
        if (selectedItemSlot)
        {
            selectedItemSlot.SetSlotSelection(false);
            selectedItemSlot = null;
        }
        selectedSlotUpdated?.Invoke();
    }

    public ActiveSlot GetActiveSlotByPosition(Vector2 itemPosition)
    { 
        foreach (var activeSlot in activeSlots)
        {
            if (activeSlot.slotView.IsPositionInSlot(itemPosition))
                return activeSlot;
        }
        return null;
    }

    public void MoveItemToSlot(ItemIcon itemIcon, ActiveSlot targetActiveSlot)
    {
        var currentActiveSlot = GetActiveSlotByItemIcon(itemIcon);

        var targetSlotView = targetActiveSlot?.slotView;   
        var currentSlotView = currentActiveSlot?.slotView;   

        var targetSlot = targetSlotView ? targetSlotView.Slot : null;
        var currentSlot = currentSlotView ? currentSlotView.Slot : null;

        var result = ModelUtils.TryMoveFromSlotToSlotWithResult(currentActiveSlot?.container, currentSlot, targetActiveSlot?.container, targetSlot);

        if (result == TransferResult.Added || result == TransferResult.Swapped || result == TransferResult.Merged)
        {
            SetSelectedSlotView(targetSlotView);
            if (result == TransferResult.Merged && itemIcon.ItemStack.ItemsNumber != 0)
                currentSlotView.SetIconInSlotPosition();
        }
        else
        {
            currentSlotView.SetIconInSlotPosition();
        }
    }
}
