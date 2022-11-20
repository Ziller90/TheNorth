using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject mobileIngameInterface;
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        mobileIngameInterface.SetActive(false);
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        mobileIngameInterface.SetActive(true);
    }
}
