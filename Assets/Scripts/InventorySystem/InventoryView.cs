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
        mainWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack));
        mainWeaponSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveMainWeapon());
        itemsViewManager.AddActiveSlot(mainWeaponSlot);

        InstantiateItemIcon(inventory.SecondaryWeaponItemStack, secondaryWeaponSlot);
        secondaryWeaponSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInEquipmentPosition(icon.ItemStack));
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

        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SuitableSlotTypes.MainWeapon)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, mainWeaponSlot);
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SuitableSlotTypes.SecondaryWeapon)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, secondaryWeaponSlot);
        if (selectedItemIcon.ItemStack.Item.SuitableSlots == SuitableSlotTypes.QuikAcess)
            inventory.UseItem(selectedItemIcon.ItemStack);

        itemsViewManager.RemoveSelection();
    }
    public void DropSelectedItem()
    {
        inventory.DropItemsStack(itemsViewManager.SelectedItemIcon.ItemStack);
        itemsViewManager.SelectedItemSlot.DestroyItemIcon();
        itemsViewManager.RemoveSelection();
    }
    public void DivideSelectedItem()
    {
        itemsViewManager.DivideSelectedItem();
    }
}
