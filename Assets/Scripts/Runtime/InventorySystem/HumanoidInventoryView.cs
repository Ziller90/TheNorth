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
    [SerializeField] Button divideButton;
    [SerializeField] MoneyView moneyView;

    HumanoidInventoryContainer inventory;
    ItemsManagerWindow itemsViewManager;

    void Awake()
    {
        itemsViewManager = Links.instance.currentItemsViewManager;
        inventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventoryContainer>();
        containerGridView.SetSlotsGroup(inventory, inventory.BackpackSlots);
        moneyView.SetHumanoidInventory(inventory);
        SetEquipSlotsView();
    }

    void SetEquipSlotsView()
    {
        mainWeaponSlotView.SetSlot(inventory.MainWeaponSlot);
        secondaryWeaponSlotView.SetSlot(inventory.SecondaryWeaponSlot);

        for (int i = 0; i < quickSlotViews.Length; i++)
            quickSlotViews[i].SetSlot(inventory.QuickAccessSlots.Slots[i]);
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

            var selectedSlot = itemsViewManager.SelectedItemSlot;
            if (inventory.CanDivideItemStackInSlot(selectedSlot.Slot))
                divideButton.interactable = true;
        }
        else
        {
            useButton.interactable = false;
            dropButton.interactable = false;
            divideButton.interactable = false;
        }
    }

    public void ClearInventory()
    {
        containerGridView.ClearContainerGrid();
        itemsViewManager.Clear();
    }

    void DrawInventorySlots()
    {
        containerGridView.DrawContainerSlots();
        itemsViewManager.AddActiveSlot(inventory, mainWeaponSlotView);
        itemsViewManager.AddActiveSlot(inventory, secondaryWeaponSlotView);

        for (int i = 0; i < quickSlotViews.Length; i++)
            itemsViewManager.AddActiveSlot(inventory, quickSlotViews[i]);
    }

    public void UseSelectedSlotItem()
    {
        var selectedSlotView = itemsViewManager.SelectedItemSlot;
        inventory.UseItemInSlot(selectedSlotView.Slot);
    }

    public void DropSelectedSlotItem()
    {
        var selectedSlotView = itemsViewManager.SelectedItemSlot;
        inventory.DropItemsStackFromSlot(selectedSlotView.Slot);
        selectedSlotView.PullOutAndDestroyItemIcon();
        itemsViewManager.RemoveSelection();
    }

    public void DivideSelectedSlotItem()
    {
        var selectedSlotView = itemsViewManager.SelectedItemSlot;
        inventory.DivideItemStackInSlot(selectedSlotView.Slot);
    }
}
