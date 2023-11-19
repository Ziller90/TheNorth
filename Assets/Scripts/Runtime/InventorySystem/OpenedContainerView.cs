using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedContainerView : MonoBehaviour
{
    [SerializeField] ContainerGridView playerInventoryGrid;
    [SerializeField] ContainerGridView openedContainerGrid;
    [SerializeField] ItemsViewManager itemsViewManager;

    HumanoidInventory playerInventory;
    void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
        playerInventoryGrid.SetContainer(playerInventory.InventoryContainer, itemsViewManager);
    }
    public void SetOpenedContainer(Container openedContainer)
    {
        openedContainerGrid.SetContainer(openedContainer, itemsViewManager);
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
