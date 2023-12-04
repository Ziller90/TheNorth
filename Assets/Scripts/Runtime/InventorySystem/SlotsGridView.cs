using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsGridView : MonoBehaviour
{
    [SerializeField] List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] GridLayoutGroup slotsGrid;
    [SerializeField] SlotView slotPrefab;

    ItemsManagerWindow itemsViewManager;
    ContainerBase slotsOwnerContainer;
    SlotGroup slotGroup;

    public void Awake()
    {
        itemsViewManager = Links.instance.currentItemsViewManager;
    }

    public void SetSlotsGroup(ContainerBase container, SlotGroup slotGroup)
    {
        slotsOwnerContainer = container;
        this.slotGroup = slotGroup;
    }

    public void ClearContainerGrid()
    {
        foreach(var slot in slotViews)
        {
            itemsViewManager.RemoveActiveSlot(slot);
            slot.Destroy();
        }
        slotViews.Clear();
    }

    public void DrawContainerSlots()
    {
        for (int i = 0; i < slotGroup.Slots.Length; i++)
        {
            var slot = Instantiate(slotPrefab, slotsGrid.transform);
            slot.SetSlot(slotGroup.Slots[i]);
            slotViews.Add(slot);
            itemsViewManager.AddActiveSlot(slotsOwnerContainer, slot);
        }
    }
}
