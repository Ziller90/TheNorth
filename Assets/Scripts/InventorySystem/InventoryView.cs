using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryView : MonoBehaviour
{
    [SerializeField] Slot mainWeaponSlot;
    [SerializeField] Slot secondaryWeaponSlot;
    [SerializeField] Slot[] quickAccessSlots;
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

        InstantiateItemIcon(inventory.MainWeaponItemStack, mainWeaponSlot);
        mainWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack, SlotType.MainWeapon));
        mainWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveMainWeapon());

        mainWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => BlockSecondaryWeaponSlot(icon));
        mainWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => UnBlockSecondaryWeaponSlot(icon));

        itemsViewManager.AddActiveSlot(mainWeaponSlot);

        InstantiateItemIcon(inventory.SecondaryWeaponItemStack, secondaryWeaponSlot);

        if (inventory.MainWeaponItemStack.Item && inventory.MainWeaponItemStack.Item.SuitableSlots == SlotType.TwoHanded)
            secondaryWeaponSlot.SetBlocked(true, inventory.MainWeaponItemStack.Item.Icon);

        secondaryWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack, SlotType.SecondaryWeapon));
        secondaryWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveSecondaryWeapon());
        itemsViewManager.AddActiveSlot(secondaryWeaponSlot);

        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            InstantiateItemIcon(inventory.QuickAccessItemStacks[i], quickAccessSlots[i]);
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

    public void AddItemToAccessSlot(ItemIcon icon, Slot slot)
    {
        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            if (quickAccessSlots[i] == slot)
            {
                inventory.SetItemInQuickAccessSlot(icon.ItemStack, i);
            }
        }
    }

    public void RemoveItemFromAccessSlot(ItemIcon icon, Slot slot)
    {
        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            if (quickAccessSlots[i] == slot)
            {
                inventory.RemoveItemFromQuickAccessSlot(i);
            }
        }
    }

    public void InstantiateItemIcon(ItemStack itemStack, Slot itemSlot)
    {
        if (itemStack != null && itemStack.Item != null)
        {
            var newIcon = Instantiate(itemIconPrefab, itemsViewManager.CommonIconsContainer);
            newIcon.SetItemStack(itemStack, itemsViewManager);
            itemSlot.InsertIcon(newIcon, true);
        }
    }

    public void UseSelectedItem()
    {
        var selectedItemIcon = itemsViewManager.SelectedItemIcon;

        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SlotType.BothHanded) 
        { 
            if (inventory.MainWeaponItemStack.Item == null || (inventory.SecondaryWeaponItemStack.Item != null && inventory.SecondaryWeaponItemStack.Item != null))
                itemsViewManager.MoveItemToSlot(selectedItemIcon, mainWeaponSlot);
            else if (inventory.SecondaryWeaponItemStack.Item == null)
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

    public void DropSelectedItem()
    {
        inventory.DropItemsStack(itemsViewManager.SelectedItemIcon.ItemStack);
        itemsViewManager.SelectedItemSlot.DestroyItemIcon();
        itemsViewManager.SelectedItemSlot.RemoveIcon();
        itemsViewManager.RemoveSelection();
    }

    public void DivideSelectedItem()
    {
        itemsViewManager.DivideSelectedItem();
    }
}
