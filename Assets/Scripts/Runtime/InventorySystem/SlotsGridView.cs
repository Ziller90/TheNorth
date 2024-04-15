using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsGridView : MonoBehaviour
{
    [SerializeField] List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] GridLayoutGroup slotsGrid;
    [SerializeField] SlotView slotPrefab;

    ContainerBase slotsOwnerContainer;
    SlotGroup slotGroup;

    public void SetSlotsGroup(ContainerBase container, SlotGroup slotGroup)
    {
        slotsOwnerContainer = container;
        this.slotGroup = slotGroup;
    }

    public void ClearContainerGrid()
    {
        var itemsManager = transform.FindInParents<ItemsManagerWindow>();
        foreach (var slot in slotViews)
        {
            itemsManager.RemoveActiveSlot(slot);
            slot.Destroy();
        }
        slotViews.Clear();
    }

    public void DrawContainerSlots()
    {
        var itemsManager = transform.FindInParents<ItemsManagerWindow>();
        for (int i = 0; i < slotGroup.Slots.Count; i++)
        {
            var slot = Instantiate(slotPrefab, slotsGrid.transform);
            slot.SetSlot(slotGroup.Slots[i]);
            slotViews.Add(slot);
            itemsManager.AddActiveSlot(slotsOwnerContainer, slot);
        }
    }
}
