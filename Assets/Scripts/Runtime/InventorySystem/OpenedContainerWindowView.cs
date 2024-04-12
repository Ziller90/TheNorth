using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedContainerWindowView : WindowView
{
    [SerializeField] SlotsGridView playerInventoryGrid;
    [SerializeField] SlotsGridView openedContainerGrid;

    HumanoidInventoryContainer playerInventory;
    SimpleContainer openedContainer;
    OpenedContainerPresentation presentation;

    public override void SetPresentation(MonoBehaviour presentation)
    {
        this.presentation = presentation as OpenedContainerPresentation;

        playerInventory = this.presentation.PlayerInventory;
        openedContainer = this.presentation.OpenedContainer;

        playerInventoryGrid.SetSlotsGroup(playerInventory, playerInventory.BackpackSlots);
        openedContainerGrid.SetSlotsGroup(openedContainer, openedContainer.SlotGroup);
        DrawView();
    }

    public override void HideWindow()
    {
        ClearView();
        presentation.CloseContainer(); 
        base.HideWindow();
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
