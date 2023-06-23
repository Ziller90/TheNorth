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
    InteractablesLocator itemsCollector;
    Creature player;
    void Start()
    {
        itemsCollector = Links.instance.playerCharacter.GetComponentInChildren<InteractablesLocator>();
        player = Links.instance.playerCharacter.GetComponent<Creature>();
    }
    public void RemoveSelection()
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
    public void SetSelectedIcon(ItemIcon itemIcon)
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

        if (itemIcon.Item.ItemData.ItemUsingType == ItemUsingType.RightHand)
            itemIcon.MoveToSlot(rightHandSlot);
        if (itemIcon.Item.ItemData.ItemUsingType == ItemUsingType.LeftHand)
            itemIcon.MoveToSlot(leftandSlot);
        if (itemIcon.Item.ItemData.ItemUsingType == ItemUsingType.ActiveUsable)
        {
            var itemUsing = itemIcon.Item.GetComponent<ItemUsing>();
            itemUsing.UseItem(player);

        }

    }
    public void ThrowItem()
    {
        itemsCollector.Drop(selectedIcon.Item);
        selectedIcon.DestroyItemIcon();
        RemoveSelection();
    }
    public void ThrowItem(Item item)
    {
        itemsCollector.Drop(item);
        selectedIcon.DestroyItemIcon();
        RemoveSelection();
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
