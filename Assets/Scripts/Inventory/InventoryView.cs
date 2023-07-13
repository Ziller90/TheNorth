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

    HumanoidInventory inventory;

    void Awake()
    {
        inventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
        containerGridView.SetContainer(inventory.InventoryContainer, itemsViewManager);
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

        InstantiateItemIcon(inventory.RightHandItem, rightHandSlot);
        rightHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemInRightHand(icon.Item));
        rightHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveItemFromRightHand());
        itemsViewManager.AddActiveSlot(rightHandSlot);

        InstantiateItemIcon(inventory.LeftHandItem, leftHandSlot);
        leftHandSlot.iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemInLeftHand(icon.Item));
        leftHandSlot.iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveItemFromLeftHand());
        itemsViewManager.AddActiveSlot(leftHandSlot);

        for (int i = 0; i < quickAccessSlots.Length; i++)
        {
            InstantiateItemIcon(inventory.QuickAccessItems[i], quickAccessSlots[i]);
            quickAccessSlots[i].iconInsertedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.SetItemInQuickAccessSlot(icon.Item, i));
            quickAccessSlots[i].iconRemovedEvent.AddListener((ItemIcon icon, Slot slot) => inventory.RemoveItemFromQuickAccessSlot(i));
            itemsViewManager.AddActiveSlot(quickAccessSlots[i]);
        }
    }

    public void InstantiateItemIcon(Item item, Slot itemSlot)
    {
        if (item)
        {
            var newIcon = Instantiate(itemIconPrefab, itemsViewManager.CommonIconsContainer);
            newIcon.SetItem(item, itemsViewManager);
            itemSlot.InsertIcon(newIcon, true);
        }
    }

    public void UseSelectedItem()
    {
        var usingItem = itemsViewManager.SelectedItemIcon;

        if (usingItem.Item.ItemData.ItemUsingType == ItemUsingType.RightHand)
            usingItem.MoveItemToSlot(rightHandSlot);
        if (usingItem.Item.ItemData.ItemUsingType == ItemUsingType.LeftHand)
            usingItem.MoveItemToSlot(leftHandSlot);
        if (usingItem.Item.ItemData.ItemUsingType == ItemUsingType.ActiveUsable)
            inventory.UseItem(usingItem.Item);

        itemsViewManager.RemoveSelection();
    }
    public void ThrowSelectedItem()
    {
        inventory.DropItem(itemsViewManager.SelectedItemIcon.Item);
        itemsViewManager.SelectedItemSlot.DestroyItemIcon();
        itemsViewManager.RemoveSelection();
    }
}
