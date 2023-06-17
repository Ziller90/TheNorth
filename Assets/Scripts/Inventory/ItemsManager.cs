using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] Button useButton;
    [SerializeField] Button throwButton;

    [SerializeField] InventorySlot rightHandSlot;
    [SerializeField] InventorySlot leftandSlot;

    [SerializeField] float timeToShowDesctiption;
    [SerializeField] ItemDescriptionPanel descriptionPanelPrefab;

    ItemDescriptionPanel descriptionPanel;
    ItemIcon selectedIcon;
    ItemsCollector itemsCollector;
    void Start()
    {
        itemsCollector = Links.instance.playerCharacter.GetComponentInChildren<ItemsCollector>();
    }
    public void RemoveSelectedItem()
    {
        if (selectedIcon)
            selectedIcon.Slot.Deselect();

        selectedIcon = null;

        useButton.interactable = false;
        throwButton.interactable = false;
    }
    public void InstantiateDescriptionPanel(ItemIcon itemIcon)
    {
        descriptionPanel = Instantiate(descriptionPanelPrefab, itemIcon.transform);
        descriptionPanel.transform.position = itemIcon.transform.position + new Vector3(120, -120, 0);
        descriptionPanel.SetItemData(itemIcon.Item.ItemData);
    }
    public void DestroyDescriptionPanel()
    {
        if (descriptionPanel)
            Destroy(descriptionPanel.gameObject);
    }
    public void SetSelectedItemIcon(ItemIcon itemIcon)
    {
        if (selectedIcon)
            selectedIcon.Slot.Deselect();

        selectedIcon = itemIcon;

        if (!selectedIcon.Slot.isSelected)
            selectedIcon.Slot.Select();

        throwButton.interactable = true;

        if (itemIcon.Item.ItemData.ItemUsingType != ItemUsingType.None)
            useButton.interactable = true;
        else
            useButton.interactable = false;

        StartCoroutine(WaitForDescription(itemIcon));
    }
    public void UseItem()
    {
        var itemIcon = selectedIcon;
        if (selectedIcon.Item.ItemData.ItemUsingType == ItemUsingType.RightHand)
        {
            RemoveSelectedItem();
            itemIcon.MoveToSlot(rightHandSlot);
        }
        if (selectedIcon.Item.ItemData.ItemUsingType == ItemUsingType.LeftHand)
        {
            selectedIcon.MoveToSlot(leftandSlot);
            RemoveSelectedItem();
        }
    }
    public void ThrowItem()
    {
        itemsCollector.Drop(selectedIcon.Item);
        selectedIcon.DestroyItemIcon();
        RemoveSelectedItem();
    }
    IEnumerator WaitForDescription(ItemIcon itemIcon)
    {
        yield return new WaitForSeconds(timeToShowDesctiption);
        if (selectedIcon == itemIcon && selectedIcon.HoldPressed && !selectedIcon.IsDraggerd)
        {
            InstantiateDescriptionPanel(itemIcon);
        }
    }
}
