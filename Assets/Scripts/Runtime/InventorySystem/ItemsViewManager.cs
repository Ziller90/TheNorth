using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemsViewManager : MonoBehaviour
{
    // used to contain all slotViews that are active on UI in moment
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
    public ActiveSlot GetActiveSlotBySlotView(SlotView slot) => activeSlots.FirstOrDefault(i => i.slotView == slot);

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

        switch (result)
        {
            case TransferResult.Added:
                currentSlotView.PullOutIcon();
                targetSlotView.InsertIcon(itemIcon);
                SetSelectedSlotView(targetSlotView);
                break;
            case TransferResult.Merged:
                MergeView(itemIcon, targetSlotView.ItemIcon);
                break;
            case TransferResult.Swapped:
                SwapView(currentSlotView, targetSlotView);
                break;
            default:
                currentSlotView.SetIconInSlotPosition();
                break;
        }
    }

    public void MergeView(ItemIcon itemIcon1, ItemIcon itemIcon2)
    {
        var stack1 = itemIcon1.ItemStack;

        var slotView1 = GetActiveSlotByItemIcon(itemIcon1).slotView;
        var slotView2 = GetActiveSlotByItemIcon(itemIcon2).slotView;

        if (stack1.ItemsNumber == 0)
        {
            SetSelectedSlotView(slotView2);
            slotView1.DestroyItemIcon();
        }
        else
        {
            slotView1.SetIconInSlotPosition();
            SetSelectedSlotView(slotView2);
        }
    }

    public void SwapView(SlotView slot1, SlotView slot2)
    {
        var itemIcon1 = slot1.ItemIcon;
        var itemIcon2 = slot2.ItemIcon;

        slot1.PullOutIcon();
        slot2.PullOutIcon();

        slot1.InsertIcon(itemIcon2);
        slot2.InsertIcon(itemIcon1);

        SetSelectedSlotView(slot2);
    }
}
