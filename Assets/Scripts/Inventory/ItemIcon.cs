using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image icon;
    [SerializeField] Image iconPrint;
    [SerializeField] GameObject selectionMarker;

    Item item;
    ContainerView containerView;
    ItemsManager itemsManager;
    InventorySlot slot;
    bool isDragged = false;
    bool holdPressed = false;

    public bool IsDraggerd => isDragged;
    public bool HoldPressed => holdPressed;
    public Item Item => item;
    public InventorySlot Slot => slot;

    public void SetSelectionMarkerActive(bool active)
    {
        selectionMarker.SetActive(active);
    }
    public void SetItem(Item item, InventorySlot slot, ContainerView containerView, ItemsManager itemsManager)
    {
        this.item = item;
        this.slot = slot;
        this.containerView = containerView;
        this.itemsManager = itemsManager;
        icon.sprite = item.ItemData.Icon;
        iconPrint.sprite = item.ItemData.Icon;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        gameObject.transform.parent = containerView.InventoryRoot;
        gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 1);
        itemsManager.SetSelectedIcon(this);
        holdPressed = true;
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        itemsManager.DestroyDescriptionPanel();
        slot.DestroyIconFootprint();
        gameObject.transform.parent = containerView.IconsContainer;

        var newSlot = containerView.GetSlot(gameObject.GetComponent<RectTransform>().position);
        MoveToSlot(newSlot);

        isDragged = false;
        holdPressed = false;
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        itemsManager.DestroyDescriptionPanel();
        if (isDragged == false)
        {
            slot.InstantiateIconFootprint();
        }
        isDragged = true;
        gameObject.transform.position = new Vector3(pointerEventData.position.x, pointerEventData.position.y,0);
    }
    public void SetSlot(InventorySlot newSlot)
    {
        newSlot.InsertIcon(this, false);
        slot = newSlot;
    }
    public void DestroyItemIcon()
    {
        slot.RemoveIcon();
        Destroy(gameObject);
    }
    public void MoveToSlot(InventorySlot newSlot)
    {
        if (!newSlot)
        {
            slot.InsertIcon(this, false);
            return;
        }

        bool isNewSlot = newSlot != slot;
        if (isNewSlot)
            itemsManager.RemoveSelection();

        if (newSlot.IsEmpty)
        {
            slot.RemoveIcon();
            SetSlot(newSlot);
        }
        else
        {
            newSlot.ItemIcon.SetSlot(slot);
            newSlot.RemoveIcon();
            SetSlot(newSlot);
        }

        if (isNewSlot)
            itemsManager.SetSelectedIcon(this);
    }
}
