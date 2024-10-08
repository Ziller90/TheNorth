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
    [SerializeField] Image background;
    [SerializeField] float timeToShowDesctiption;
    [SerializeField] ItemDescriptionPanel descriptionPanelPrefab;
    [SerializeField] TMP_Text itemNumberText;
    [SerializeField] Vector3 descitptionPanelOffset;
    [SerializeField] bool isInteratable = true;

    ItemDescriptionPanel descriptionPanel;
    ItemsManagerWindow itemsManager;
    ItemStack itemStack;

    public ItemStack ItemStack => itemStack;
    public bool IsInteratable
    {
        get => isInteratable; 
        set
        {
            isInteratable = value;
            icon.raycastTarget = isInteratable;
            background.raycastTarget = isInteratable;
        }
    }

    bool isDragged = false;
    bool isHold = false;

    public void SetItemStack(ItemStack itemStack)
    {
        this.itemStack = itemStack;
        icon.sprite = itemStack.Item.Icon;
        itemsManager = transform.FindInParents<ItemsManagerWindow>();
        itemStack.itemsNumberUpdatedEvent += OnItemsNumberUpdated;
        OnItemsNumberUpdated();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!IsInteratable)
            return;

        var thisIconSlot = itemsManager.GetActiveSlotByItemIcon(this).slotView;
        itemsManager.SetSelectedSlotView(thisIconSlot);
        transform.SetParent(itemsManager.CommonIconsContainer);
        isHold = true;
        StartCoroutine(WaitForDescription());
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (!IsInteratable)
            return;

        var newActiveSlot = itemsManager.GetActiveSlotByPosition(transform.position);
        itemsManager.MoveItemToSlot(this, newActiveSlot);

        isDragged = false;
        isHold = false;
        StopAllCoroutines();
        DestroyDescriptionPanel();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (!IsInteratable)
            return;

        gameObject.transform.position = new Vector3(pointerEventData.position.x, pointerEventData.position.y,0);
        if (!isDragged)
        {
            StopAllCoroutines();
            DestroyDescriptionPanel();
            isDragged = true;
            isHold = false;
        }
    }

    void OnEnable()
    {
        if (itemStack != null)
        {
            itemStack.itemsNumberUpdatedEvent -= OnItemsNumberUpdated;
            itemStack.itemsNumberUpdatedEvent += OnItemsNumberUpdated;
        }
    }

    void OnDisable()
    {
        itemStack.itemsNumberUpdatedEvent -= OnItemsNumberUpdated;
    }

    void OnItemsNumberUpdated()
    {
        itemNumberText.text = itemStack.ItemsNumber == 1 ? "" : itemStack.ItemsNumber.ToString();
    }

    IEnumerator WaitForDescription()
    {
        yield return new WaitForSeconds(timeToShowDesctiption);
        if (isHold && !isDragged)
        {
            InstantiateDescriptionPanel(this);
        }
    }

    void InstantiateDescriptionPanel(ItemIcon itemIcon)
    {
        descriptionPanel = Instantiate(descriptionPanelPrefab, itemIcon.transform);
        descriptionPanel.transform.position = itemIcon.transform.position + descitptionPanelOffset;
        descriptionPanel.SetItemData(itemIcon.ItemStack.Item);
    }

    void DestroyDescriptionPanel()
    {
        if (descriptionPanel)
            Destroy(descriptionPanel.gameObject);
    }
}
