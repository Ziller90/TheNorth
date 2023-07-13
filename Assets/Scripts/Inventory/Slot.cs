using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    ItemIcon itemIconInSlot;
    RectTransform slotTransform;
    public bool IsEmpty => itemIconInSlot == null;
    public ItemIcon ItemIcon => itemIconInSlot;

    public UnityEvent<ItemIcon, Slot> iconInsertedEvent;
    public UnityEvent<ItemIcon, Slot> iconRemovedEvent;

    [SerializeField] bool slotIsRhombus;
    [SerializeField] Image iconShadow;
    [SerializeField] Image selectionMarker;
    private void OnEnable()
    {
        slotTransform = GetComponent<RectTransform>();
    }
    public void InsertIcon(ItemIcon icon, bool initializing)
    {
        if (!initializing)
            iconInsertedEvent?.Invoke(icon, this);

        StartCoroutine(SetIconInSlotPosition(icon));
    }
    public void RemoveIcon()
    {
        iconRemovedEvent?.Invoke(itemIconInSlot, this);
        itemIconInSlot = null;
    }
    public IEnumerator SetIconInSlotPosition(ItemIcon icon)
    {
        itemIconInSlot = icon;
        yield return new WaitForEndOfFrame();
        itemIconInSlot.GetComponent<RectTransform>().position = slotTransform.position;
        icon.transform.parent = slotTransform;
    }
    public bool IsPositionInSlot(Vector2 position)
    {
        Vector3[] corners = new Vector3[4];
        slotTransform.GetWorldCorners(corners);
        if (!slotIsRhombus) 
        {
            Rect slotWorldRect = new Rect(corners[0].x, corners[0].y, corners[3].x - corners[0].x, corners[1].y - corners[0].y);
            if (slotWorldRect.Contains(position))
                return true;
        }
        else if (slotIsRhombus)
        {
            float rhombusSize = (corners[0] - corners[2]).magnitude;
            if (Utils.isInRhombus(position, slotTransform.position, rhombusSize, rhombusSize))
                return true;
        }
        return false;
    }

    public void SetSlotSelection(bool isSelected)
    {
        if (isSelected)
            selectionMarker.gameObject.SetActive(true);
        if (!isSelected)
            selectionMarker.gameObject.SetActive(false);
    }
    public void ShowItemShadow(ItemIcon itemIcon)
    {
        iconShadow.gameObject.SetActive(true);
        iconShadow.sprite = itemIcon.Item.ItemData.Icon;
    }
    public void HideItemShadow()
    {
        iconShadow.gameObject.SetActive(false);
    }
    public void Destroy()
    {
        DestroyItemIcon();
        Destroy(gameObject);
    }
    public void DestroyItemIcon()
    {
        if (itemIconInSlot)
            Destroy(itemIconInSlot.gameObject);
    }
}
