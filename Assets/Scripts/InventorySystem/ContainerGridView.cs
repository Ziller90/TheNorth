using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerGridView : MonoBehaviour
{
    [SerializeField] List<Slot> slots = new List<Slot>();
    [SerializeField] GridLayoutGroup slotsGrid;
    [SerializeField] ItemIcon itemIconPrefab;
    [SerializeField] Slot slotPrefab;
    ItemsViewManager itemsViewManager;
    Container container;
    public void SetContainer(Container container, ItemsViewManager itemsViewManager)
    {
        this.container = container;
        this.itemsViewManager = itemsViewManager;
    }
    public void ClearContainerGrid()
    {
        foreach(var slot in slots)
        {
            itemsViewManager.RemoveActiveSlot(slot);
            slot.Destroy();
        }
        slots.Clear();
    }
    public void DrawContainerSlots()
    {
        for (int i = 0; i < container.MaxItemStacksNumber; i++)
        {
            var slot = Instantiate(slotPrefab, slotsGrid.transform);
            var itemsStack = container.GetItemStack(i);
            if (itemsStack != null && itemsStack.Item != null)
            {
                var newIcon = Instantiate(itemIconPrefab, slot.transform);
                newIcon.SetItemStack(itemsStack, itemsViewManager);
                slot.InsertIcon(newIcon, true);
            }
            slot.iconInsertedEvent.AddListener(AddItemToContainer);
            slot.iconRemovedEvent.AddListener(RemoveItemFromContainer);
            slots.Add(slot);
            itemsViewManager.AddActiveSlot(slot);
        }
    }

    public void AddItemToContainer(ItemIcon icon, Slot slot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == slot)
            {
                container.AddItemsStackInIndex(icon.ItemStack, i);
            }
        }
    }
    public void RemoveItemFromContainer(ItemIcon icon, Slot slot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == slot)
            {
                container.RemoveItemStackAtIndex(i);
            }
        }
    }
}
