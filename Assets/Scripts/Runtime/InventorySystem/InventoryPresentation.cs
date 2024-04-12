using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener inventoryWindowOpener;

    HumanoidInventoryContainer playerInventory;

    public HumanoidInventoryContainer PlayerInventory => playerInventory;

    public void OpenInventory()
    {
        playerInventory = Game.GameSceneInitializer.Player.GetComponentInChildren<HumanoidInventoryContainer>();
        inventoryWindowOpener.ShowWindow();
    }
}
