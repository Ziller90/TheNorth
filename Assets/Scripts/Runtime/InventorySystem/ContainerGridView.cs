using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGridView : MonoBehaviour
{
    [SerializeField] List<SlotView> slotViews = new List<SlotView>();
    [SerializeField] GridLayoutGroup slotsGrid;
    [SerializeField] SlotView slotPrefab;
    ItemsViewManager itemsViewManager;
    Container container;

    public void SetContainer(Container container, ItemsViewManager itemsViewManager)
    {
        this.container = container;
        this.itemsViewManager = itemsViewManager;
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
        for (int i = 0; i < container.Slots.Length; i++)
        {
            var slot = Instantiate(slotPrefab, slotsGrid.transform);

            slot.SetSlot(container.Slots[i], itemsViewManager);
            slot.iconInsertedEvent.AddListener(AddItemToContainer);
            slot.iconRemovedEvent.AddListener(RemoveItemFromContainer);
            slotViews.Add(slot);
            itemsViewManager.AddActiveSlot(slot);
        }
    }

    public void AddItemToContainer(ItemIcon icon, SlotView slotView)
    {
        slotView.Slot.SetItemStack(icon.ItemStack);
    }

    public void RemoveItemFromContainer(ItemIcon icon, SlotView slotView)
    {
        slotView.Slot.SetEmpty();
    }
}
