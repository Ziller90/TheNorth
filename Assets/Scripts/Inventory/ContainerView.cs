using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerView : MonoBehaviour
{
    Container container;
    Equipment equipment;
    [SerializeField] DescriptionShower descriptionShower;
    [SerializeField] List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField] Transform iconsContainer;
    [SerializeField] GridLayoutGroup slotsGrid;
    [SerializeField] GameObject iconTemplatePrefab;
    [SerializeField] GameObject inventorySlotPrefab;
    [SerializeField] Transform inventoryRoot;

    [SerializeField] InventorySlot rightHandSlot;
    [SerializeField] InventorySlot leftHandSlot;

    [SerializeField] InventorySlot[] quickAccessSlots;

    public DescriptionShower DescriptionShower => descriptionShower;
    public Container Container => container;
    public GridLayoutGroup GridLayoutGroup => slotsGrid;
    public Transform InventoryRoot => inventoryRoot;
    public Transform IconsContainer => iconsContainer;

    public void Start()
    {
        rightHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, InventorySlot slot)
            => equipment.SetItemInRightHand(icon.Item));

        rightHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, InventorySlot slot)
            => equipment.RemoveItemFromRightHand());

        leftHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, InventorySlot slot)
            => equipment.SetItemInLeftHand(icon.Item));

        leftHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, InventorySlot slot)
            => equipment.RemoveItemFromLeftHand());
    }

    private void OnEnable()
    {
        container = Links.instance.playerCharacter.GetComponentInChildren<Container>();
        equipment = Links.instance.playerCharacter.GetComponentInChildren<Equipment>();

        for (int i = 0; i < equipment.QuickAccessItems.Length; i++)
        {
            quickAccessSlots[i].iconInsertedEvent.AddListener((ItemIcon icon, InventorySlot slot)
                => equipment.SetItemInQuickAccessSlot(icon.Item, i));

            quickAccessSlots[i].iconRemovedEvent.AddListener((ItemIcon icon, InventorySlot slot)
                => equipment.RemoveItemFromQuickAccessSlot(i));
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        ClearInventory();
        DrawInventorySlots();
    }

    public void ClearInventory()
    {
        foreach (var slot in inventorySlots)
        {
            slot.Destroy();
        }
        inventorySlots.Clear();
    }

    public void AddItemToContainer(ItemIcon icon, InventorySlot slot)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == slot)
            {
                container.AddItemInIndex(icon.Item, i);
            }
        }
    }

    public void RemoveItemFromContainer(ItemIcon icon, InventorySlot slot)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == slot)
            {
                container.RemoveItem(icon.Item, i);
            }
        }
    }

    void DrawInventorySlots()
    {
        for (int i = 0; i < container.MaxItemsNumber; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, slotsGrid.transform).GetComponent<InventorySlot>();

            var item = container.GetItem(i);
            if (item)
            {
                var newIcon = Instantiate(iconTemplatePrefab, iconsContainer).GetComponent<ItemIcon>();
                newIcon.SetItem(item, slot, this);
                slot.InsertIcon(newIcon, true);
            }
            slot.iconInsertedEvent.AddListener(AddItemToContainer);
            slot.iconRemovedEvent.AddListener(RemoveItemFromContainer);
            inventorySlots.Add(slot);
        }
    }

    public InventorySlot GetSlot(Vector2 itemPosition)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.IsPositionInSlot(itemPosition))
                return slot;
        }

        foreach (var slot in quickAccessSlots)
        {
            if (slot.IsPositionInSlot(itemPosition))
                return slot;
        }

        if (rightHandSlot.IsPositionInSlot(itemPosition))
            return rightHandSlot;
        if (leftHandSlot.IsPositionInSlot(itemPosition))
            return leftHandSlot;

        return null;
    }
}
