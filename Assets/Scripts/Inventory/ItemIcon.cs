using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image icon;

    Item item;
    ContainerView containerView;
    DescriptionShower descriptionShower;
    InventorySlot slot;
    bool isSelected;

    public Item Item => item;
    void Start()
    {

    }
    public void SetItem(Item item, InventorySlot slot, ContainerView containerView)
    {
        this.item = item;
        this.slot = slot;
        this.containerView = containerView;

        icon.sprite = item.ItemData.Icon;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        containerView.DescriptionShower.SetSelectedItemIcon(this);
        gameObject.transform.parent = containerView.InventoryRoot;
        gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 1);
        isSelected = true;
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        gameObject.transform.parent = containerView.IconsContainer;
        var newSlot = containerView.GetSlot(gameObject.GetComponent<RectTransform>().position);
        if (newSlot)
        {
            if (newSlot.IsEmpty)
            {
                slot.RemoveIcon();
                MoveToSlot(newSlot);
            }
            else
            {
                newSlot.ItemIcon.MoveToSlot(slot);
                newSlot.RemoveIcon();
                MoveToSlot(newSlot);
            }
        }
        else
        {
            slot.InsertIcon(this, false);
        }
        isSelected = false;
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        gameObject.transform.position = new Vector3(pointerEventData.position.x, pointerEventData.position.y,0);
    }
    public void MoveToSlot(InventorySlot newSlot)
    {
        newSlot.InsertIcon(this, false);
        slot = newSlot;
    }
    public void DestroyItemIcon()
    {
        slot.RemoveIcon();
        Destroy(gameObject);
    }
}
