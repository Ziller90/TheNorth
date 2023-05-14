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

    private void OnEnable()
    {
        container = Links.instance.playerCharacter.GetComponentInChildren<Container>();
        equipment = Links.instance.playerCharacter.GetComponentInChildren<Equipment>();
        DrawInventory();
    }

    public void DrawInventory()
    {
        ClearInventory();
        DrawInventorySlots();
        FillEquipmentSlots();
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
            slot.SlotColliderSize = slotsGrid.cellSize.x + slotsGrid.spacing.x;

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
    void FillEquipmentSlots()
    {
        if (equipment.RightHandItem) 
        {
            var rightHandIcon = Instantiate(iconTemplatePrefab, iconsContainer).GetComponent<ItemIcon>();
            rightHandIcon.SetItem(equipment.RightHandItem, rightHandSlot, this);
        }
        if (equipment.LeftHandItem)
        {
            var leftHandIcon = Instantiate(iconTemplatePrefab, iconsContainer).GetComponent<ItemIcon>();
            leftHandIcon.SetItem(equipment.LeftHandItem, leftHandSlot, this);
        }
        for(int i = 0; i < equipment.QuickAccessItems.Length; i++)
        {
            if (equipment.QuickAccessItems[i])
            {
                var quickAccessIcon = Instantiate(iconTemplatePrefab, iconsContainer).GetComponent<ItemIcon>();
                quickAccessIcon.SetItem(equipment.QuickAccessItems[i], quickAccessSlots[i], this);
            }
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
    private void Update()
    {
        foreach(var slot in inventorySlots)
        {
            Debug.Log(slot.name + " " + slot.GetComponent<RectTransform>().position);
        }
    }
}
