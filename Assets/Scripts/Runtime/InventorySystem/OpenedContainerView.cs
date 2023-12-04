using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedContainerView : MonoBehaviour
{
    [SerializeField] SlotsGridView playerInventoryGrid;
    [SerializeField] SlotsGridView openedContainerGrid;
    [SerializeField] ItemsViewManager itemsViewManager;

    HumanoidInventoryContainer playerInventory;

    void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventoryContainer>();
        playerInventoryGrid.SetSlotsGroup(playerInventory, itemsViewManager, playerInventory.BackpackSlots);
    }

    public void SetOpenedContainer(SimpleContainer openedContainer)
    {
        openedContainerGrid.SetSlotsGroup(openedContainer, itemsViewManager, openedContainer.SlotGroup);
    }

    void OnEnable()
    {
        DrawView();
    }

    void OnDisable()
    {
        ClearView();
    }

    void DrawView()
    {
        playerInventoryGrid.DrawContainerSlots();
        openedContainerGrid.DrawContainerSlots();
    }
    void ClearView()
    {
        playerInventoryGrid.ClearContainerGrid();
        openedContainerGrid.ClearContainerGrid();
    }
}
