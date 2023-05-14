using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DescriptionShower : MonoBehaviour
{
    public TMP_Text descriptionText;
    public Image bottomPanelItemIcon;
    public GameObject BottomPanel;
    public GameObject DisabledBottomPanel;

    public Button equipButton;
    public Button throwButton;

    public ItemIcon selectedIcon;

    ItemsCollector itemsCollector;
    void Start()
    {
        itemsCollector = Links.instance.playerCharacter.GetComponentInChildren<ItemsCollector>();

        DisableBottomPanel();
        equipButton.onClick.AddListener(EquipItem);
        throwButton.onClick.AddListener(ThrowItem);
    }
    public void ShowDescription(ItemIcon itemIcon)
    {
        DisabledBottomPanel.SetActive(false);
        BottomPanel.SetActive(true);
        descriptionText.text = itemIcon.Item.ItemData.Description;
        bottomPanelItemIcon.sprite = itemIcon.Item.ItemData.Icon;
    }
    public void SetSelectedItemIcon(ItemIcon itemIcon)
    {
        selectedIcon = itemIcon;
        ShowDescription(itemIcon);
    }
    public void DisableBottomPanel()
    {
        DisabledBottomPanel.SetActive(true);
        BottomPanel.SetActive(false);
    }
    public void EquipItem()
    {

    }
    public void ThrowItem()
    {
        selectedIcon.DestroyItemIcon();
        itemsCollector.Drop(selectedIcon.Item);
    }
}
