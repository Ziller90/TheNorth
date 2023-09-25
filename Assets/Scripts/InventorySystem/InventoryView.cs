using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryView : MonoBehaviour
{
    [SerializeField] Slot rightHandSlot;
    [SerializeField] Slot leftHandSlot;
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

        rightHandSlot.DestroyItemIcon();
        rightHandSlot.iconInsertedEvent.RemoveAllListeners();
        rightHandSlot.iconRemovedEvent.RemoveAllListeners();
        itemsViewManager.RemoveActiveSlot(rightHandSlot);

        leftHandSlot.DestroyItemIcon();
        leftHandSlot.iconInsertedEvent.RemoveAllListeners();
        leftHandSlot.iconRemovedEvent.RemoveAllListeners();
        itemsViewManager.RemoveActiveSlot(leftHandSlot);
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

        InstantiateItemIcon(inventory.RightHandItemStack, rightHandSlot);
        rightHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInRightHand(icon.ItemStack));
        rightHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveItemFromRightHand());
        itemsViewManager.AddActiveSlot(rightHandSlot);

        InstantiateItemIcon(inventory.LeftHandItemStack, leftHandSlot);
        leftHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemStackInLeftHand(icon.ItemStack));
        leftHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveItemFromLeftHand());
        itemsViewManager.AddActiveSlot(leftHandSlot);

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

        if (selectedItemIcon.ItemStack.Item.ItemUsingType == ItemUsingType.RightHand)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, rightHandSlot);
        if (selectedItemIcon.ItemStack.Item.ItemUsingType == ItemUsingType.LeftHand)
            itemsViewManager.MoveItemToSlot(selectedItemIcon, leftHandSlot);
        if (selectedItemIcon.ItemStack.Item.ItemUsingType == ItemUsingType.ActiveUsable)
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
