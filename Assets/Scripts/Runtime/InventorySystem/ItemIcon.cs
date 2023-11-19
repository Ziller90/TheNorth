using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Data;
using System.Collections.Generic;

public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image icon;
    [SerializeField] float timeToShowDesctiption;
    [SerializeField] ItemDescriptionPanel descriptionPanelPrefab;
    [SerializeField] TMP_Text itemNumberText;
    [SerializeField] Vector3 descitptionPanelOffset;

    ItemDescriptionPanel descriptionPanel;
    ItemsViewManager itemsViewManager;
    ItemStack itemStack;

    public ItemStack ItemStack => itemStack;

    bool isDragged = false;
    bool isHold = false;

    void OnEnable()
    {
        if (itemStack != null)
        {
            itemStack.itemsNumberUpdatedEvent -= OnItemsNumberUpdated;
            itemStack.itemsNumberUpdatedEvent += OnItemsNumberUpdated;

            itemStack.deletedEvent -= OnItemStackDeleted;
            itemStack.deletedEvent += OnItemStackDeleted;
        }
    }

    void OnDisable()
    {
        itemStack.itemsNumberUpdatedEvent -= OnItemsNumberUpdated;
        itemStack.deletedEvent -= OnItemStackDeleted;
    }

    void OnItemsNumberUpdated()
    {
        itemNumberText.text = itemStack.ItemsNumber == 1 ? "" : itemStack.ItemsNumber.ToString();
    }
    void OnItemStackDeleted()
    {
        var currentSlot = itemsViewManager.GetItemIconSlot(this);
        currentSlot.DestroyItemIcon();
    }

    public void SetItemStack(ItemStack itemStack, ItemsViewManager itemsViewManager)
    {
        this.itemStack = itemStack;
        this.itemsViewManager = itemsViewManager;
        icon.sprite = itemStack.Item.Icon;

        itemStack.itemsNumberUpdatedEvent += OnItemsNumberUpdated;
        itemStack.deletedEvent += OnItemStackDeleted;
        OnItemsNumberUpdated();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        itemsViewManager.SetSelectedIcon(this);
        transform.SetParent(itemsViewManager.CommonIconsContainer);
        isHold = true;
        StartCoroutine(WaitForDescription());
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        var newSlot = itemsViewManager.GetSlotByPosition(transform.position);
        itemsViewManager.MoveItemToSlot(this, newSlot);

        isDragged = false;
        isHold = false;
        StopAllCoroutines();
        DestroyDescriptionPanel();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        gameObject.transform.position = new Vector3(pointerEventData.position.x, pointerEventData.position.y,0);
        if (!isDragged)
        {
            StopAllCoroutines();
            DestroyDescriptionPanel();
            isDragged = true;
            isHold = false;
        }
    }

    IEnumerator WaitForDescription()
    {
        yield return new WaitForSeconds(timeToShowDesctiption);
        if (isHold && !isDragged)
        {
            InstantiateDescriptionPanel(this);
        }
    }

    public void InstantiateDescriptionPanel(ItemIcon itemIcon)
    {
        descriptionPanel = Instantiate(descriptionPanelPrefab, itemIcon.transform);
        descriptionPanel.transform.position = itemIcon.transform.position + descitptionPanelOffset;
        descriptionPanel.SetItemData(itemIcon.ItemStack.Item);
    }

    public void DestroyDescriptionPanel()
    {
        if (descriptionPanel)
            Destroy(descriptionPanel.gameObject);
    }
}
