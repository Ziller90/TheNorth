using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidInventoryView : MonoBehaviour
{
    [SerializeField] SlotView mainWeaponSlotView;
    [SerializeField] SlotView secondaryWeaponSlotView;
    [SerializeField] SlotView[] quickSlotViews;
    [SerializeField] SlotsGridView containerGridView;
    [SerializeField] ItemIcon itemIconPrefab;
    [SerializeField] Button useButton;
    [SerializeField] Button dropButton;
    [SerializeField] MoneyView moneyView;

    HumanoidInventoryContainer inventory;
    ItemsManagerWindow itemsViewManager;

    void Awake()
    {
        itemsViewManager = Links.instance.currentItemsViewManager;
        inventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventoryContainer>();
        containerGridView.SetSlotsGroup(inventory, inventory.BackpackSlots);
        moneyView.SetHumanoidInventory(inventory);
    }

    private void OnEnable()
    {
        DrawInventorySlots();
        itemsViewManager.selectedSlotUpdated += UpdateInventoryButtonsView;
    }

    private void OnDisable()
    {
        useButton.interactable = false;
        dropButton.interactable = false;

        itemsViewManager.selectedSlotUpdated -= UpdateInventoryButtonsView;
        ClearInventory();
    }

    void UpdateInventoryButtonsView()
    {
        if (itemsViewManager.SelectedItemIcon != null)
        {
            useButton.interactable = true;
            dropButton.interactable = true;
        }
        else
        {
            useButton.interactable = false;
            dropButton.interactable = false;
        }
    }

    public void ClearInventory()
    {
        containerGridView.ClearContainerGrid();
        mainWeaponSlotView.DestroyItemIcon();
        secondaryWeaponSlotView.DestroyItemIcon();

        foreach (var quickSlot in quickSlotViews)
            quickSlot.DestroyItemIcon();

        itemsViewManager.Clear();
    }

    void DrawInventorySlots()
    {
        containerGridView.DrawContainerSlots();

        mainWeaponSlotView.SetSlot(inventory.MainWeaponSlot);
        itemsViewManager.AddActiveSlot(inventory, mainWeaponSlotView);

        secondaryWeaponSlotView.SetSlot(inventory.SecondaryWeaponSlot);
        itemsViewManager.AddActiveSlot(inventory, secondaryWeaponSlotView);

        for (int i = 0; i < quickSlotViews.Length; i++)
        {
            quickSlotViews[i].SetSlot(inventory.QuickAccessSlots.Slots[i]);
            itemsViewManager.AddActiveSlot(inventory, quickSlotViews[i]);
        }
    }

    public void UseSelectedItem()
    {
        inventory.UseItemInSlot(itemsViewManager.SelectedItemSlot.Slot);
    }

    public void DropSelectedSlotItem()
    {
        inventory.DropItemsStackFromSlot(itemsViewManager.SelectedItemSlot.Slot);
        itemsViewManager.SelectedItemSlot.PullOutAndDestroyItemIcon();
        itemsViewManager.RemoveSelection();
    }

    public void DivideSelectedItem()
    {
        
    }
}
