using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionShower : MonoBehaviour
{
    public TMP_Text descriptionText;
    public Image bottomPanelItemIcon;
    public GameObject BottomPanel;
    public GameObject DisabledBottomPanel;

    public Button equipButton;
    public Button throwButton;

    public ItemIcon selectedItem;

    public ItemsCollector itemsCollector;
    void Start()
    {
        DisableBottomPanel();
        equipButton.onClick.AddListener(EquipItem);
        throwButton.onClick.AddListener(ThrowItem);
    }
    public void ShowDescription(ItemIcon itemIcon)
    {
        DisabledBottomPanel.SetActive(false);
        BottomPanel.SetActive(true);
        descriptionText.text = itemIcon.item.itemData.description;
        bottomPanelItemIcon.sprite = itemIcon.item.itemData.icon;
    }
    public void SetSelectedItemIcon(ItemIcon itemIcon)
    {
        selectedItem = itemIcon;
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
        Destroy(selectedItem.gameObject);
        itemsCollector.Drop(selectedItem.item);
    }
}
