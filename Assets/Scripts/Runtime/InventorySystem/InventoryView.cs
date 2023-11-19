using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryView : MonoBehaviour
{
    [SerializeField] SlotView mainWeaponSlot;
    [SerializeField] SlotView secondaryWeaponSlot;
    [SerializeField] SlotView[] quickAccessSlots;
    [SerializeField] ContainerGridView containerGridView;
    [SerializeField] ItemIcon itemIconPrefab;
    [SerializeField] ItemsViewManager itemsViewManager;
    [SerializeField] Button useButton;
    [SerializeField] Button throwButton;
    [SerializeField] MoneyView moneyView;

    HumanoidInventory inventory;

    void Awake()
    {
        inventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
        containerGridView.SetContainer(inventory.InventoryContainer, itemsViewManager);
        moneyView.SetHumanoidInventory(inventory);
    }
    private void OnEnable()
    {
        DrawInventorySlots();
        itemsViewManager.selectedItemIconUpdated += UpdateInventoryButtonsView;
    }

    void UpdateInventoryButtonsView()
    {
        if (itemsViewManager.SelectedItemIcon != null)
        {
            useButton.interactable = true;
            throwButton.interactable = true;
        }
        else
        {
            useButton.interactable = false;
            throwButton.interactable = false;
        }
    }

    private void OnDisable()
    {
        useButton.interactable = false;
        throwButton.interactable = false;

        itemsViewManager.selectedItemIconUpdated -= UpdateInventoryButtonsView;
        ClearInventory();
    }

    public void ClearInventory()
    {
        containerGridView.ClearContainerGrid();

        mainWeaponSlot.DestroyItemIcon();
        mainWeaponSlot.iconInsertedEvent.RemoveAllListeners();
        mainWeaponSlot.iconRemovedEvent.RemoveAllListeners();
        itemsViewManager.RemoveActiveSlot(mainWeaponSlot);

        secondaryWeaponSlot.DestroyItemIcon();
        secondaryWeaponSlot.iconInsertedEvent.RemoveAllListeners();
        secondaryWeaponSlot.iconRemovedEvent.RemoveAllListeners();
        itemsViewManager.RemoveActiveSlot(secondaryWeaponSlot);
        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            itemsViewManager.RemoveActiveSlot(quickAccessSlots[i]);
            quickAccessSlots[i].iconInsertedEvent.RemoveAllListeners();
            quickAccessSlots[i].iconRemovedEvent.RemoveAllListeners();
            quickAccessSlots[i].DestroyItemIcon();
        }
    }

    void DrawInventorySlots()
    {
        containerGridView.DrawContainerSlots();

        mainWeaponSlot.SetSlot(inventory.MainWeaponSlot, itemsViewManager);
        mainWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, SlotView slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack, SlotType.MainWeapon));
        mainWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, SlotView slot) => inventory.RemoveMainWeapon());

        mainWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, SlotView slot) => BlockSecondaryWeaponSlot(icon));
        mainWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, SlotView slot) => UnBlockSecondaryWeaponSlot(icon));

        itemsViewManager.AddActiveSlot(mainWeaponSlot);

        secondaryWeaponSlot.SetSlot(inventory.SecondaryWeaponSlot, itemsViewManager);

        if (!inventory.MainWeaponSlot.isEmpty && inventory.MainWeaponSlot.ItemStack.Item.SuitableSlots == SlotType.TwoHanded)
            secondaryWeaponSlot.SetBlocked(true, inventory.MainWeaponSlot.ItemStack.Item.Icon);

        secondaryWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, SlotView slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack, SlotType.SecondaryWeapon));
        secondaryWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, SlotView slot) => inventory.RemoveSecondaryWeapon());
        itemsViewManager.AddActiveSlot(secondaryWeaponSlot);

        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            quickAccessSlots[i].SetSlot(inventory.QuickAccessSlots[i], itemsViewManager);
            quickAccessSlots[i].iconInsertedEvent.AddListener(AddItemToAccessSlot);
            quickAccessSlots[i].iconRemovedEvent.AddListener(RemoveItemFromAccessSlot);
            itemsViewManager.AddActiveSlot(quickAccessSlots[i]);
        }
    }

    void BlockSecondaryWeaponSlot(ItemIcon itemIcon)
    {
        if (itemIcon.ItemStack.Item.SuitableSlots == SlotType.TwoHanded)
            secondaryWeaponSlot.SetBlocked(true, itemIcon.ItemStack.Item.Icon);
    }

    void UnBlockSecondaryWeaponSlot(ItemIcon itemIcon)
    {
        if (itemIcon.ItemStack.Item.SuitableSlots == SlotType.TwoHanded)
            secondaryWeaponSlot.SetBlocked(false);
    }

    public void AddItemToAccessSlot(ItemIcon icon, SlotView slot)
    {
        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            if (quickAccessSlots[i] == slot)
            {
                inventory.SetItemInQuickAccessSlot(icon.ItemStack, i);
            }
        }
    }

    public void RemoveItemFromAccessSlot(ItemIcon icon, SlotView slot)
    {
        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            if (quickAccessSlots[i] == slot)
            {
                inventory.RemoveItemFromQuickAccessSlot(i);
            }
        }
    }

    public void UseSelectedItem()
    {
        var selectedItemIcon = itemsViewManager.SelectedItemIcon;

        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.BothHanded) 
        { 
            if (inventory.MainWeaponSlot.ItemStack.Item == null || (inventory.SecondaryWeaponSlot.ItemStack.Item != null && inventory.SecondaryWeaponSlot.ItemStack.Item != null))
                itemsViewManager.MoveItemToSlot(selectedItemIcon, mainWeaponSlot);
            else if (inventory.SecondaryWeaponSlot.ItemStack.Item == null)
                itemsViewManager.MoveItemToSlot(selectedItemIcon, secondaryWeaponSlot);
        }
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.TwoHanded)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, mainWeaponSlot);
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.MainWeapon)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, mainWeaponSlot);
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.SecondaryWeapon)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, secondaryWeaponSlot);
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.QuikAcess)
            inventory.UseItem(selectedItemIcon.ItemStack);

        itemsViewManager.RemoveSelection();
    }

    public void DropSelectedSlotItem()
    {
        inventory.DropItemsStackFromSlot(itemsViewManager.SelectedItemSlot.Slot);
        itemsViewManager.SelectedItemSlot.DestroyWithPullOut();
        itemsViewManager.RemoveSelection();
    }

    public void DivideSelectedItem()
    {
        itemsViewManager.DivideSelectedItem();
    }
}
