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
    [SerializeField] ItemsViewManager itemsViewManager;
    [SerializeField] Button useButton;
    [SerializeField] Button dropButton;
    [SerializeField] MoneyView moneyView;

    HumanoidInventoryContainer inventory;

    void Awake()
    {
        inventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventoryContainer>();
        containerGridView.SetSlotsGroup(inventory, itemsViewManager, inventory.BackpackSlots);
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
        itemsViewManager.RemoveActiveSlot(mainWeaponSlotView);

        secondaryWeaponSlotView.DestroyItemIcon();
        itemsViewManager.RemoveActiveSlot(secondaryWeaponSlotView);

        foreach (var quickSlot in quickSlotViews)
        {
            quickSlot.DestroyItemIcon();
            itemsViewManager.RemoveActiveSlot(quickSlot);
        }
    }

    void DrawInventorySlots()
    {
        containerGridView.DrawContainerSlots();

        mainWeaponSlotView.SetSlot(inventory.MainWeaponSlot, itemsViewManager);
        itemsViewManager.AddActiveSlot(inventory, mainWeaponSlotView);

        secondaryWeaponSlotView.SetSlot(inventory.SecondaryWeaponSlot, itemsViewManager);
        itemsViewManager.AddActiveSlot(inventory, secondaryWeaponSlotView);

        for (int i = 0; i < quickSlotViews.Length; i++)
        {
            quickSlotViews[i].SetSlot(inventory.QuickAccessSlots.Slots[i], itemsViewManager);
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
        itemsViewManager.SelectedItemSlot.PullOutAndDestroy();
        itemsViewManager.RemoveSelection();
    }

    public void DivideSelectedItem()
    {
        
    }
}
