using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour
{
    ItemIcon itemIconInSlot;
    RectTransform slotTransform;

    [SerializeField] float slotColliderSize;
    public bool IsEmpty => itemIconInSlot == null;
    public ItemIcon ItemIcon => itemIconInSlot;
    //public float SlotColliderSize { get => slotColliderSize; set => slotColliderSize = value; }

    public UnityEvent<ItemIcon, InventorySlot> iconInsertedEvent;
    public UnityEvent<ItemIcon, InventorySlot> iconRemovedEvent;

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
    IEnumerator SetIconInSlotPosition(ItemIcon icon)
    {
        yield return new WaitForEndOfFrame();
        itemIconInSlot = icon;
        itemIconInSlot.GetComponent<RectTransform>().position = slotTransform.position;
        icon.transform.parent = slotTransform;
    }
    public bool IsPositionInSlot(Vector2 position)
    {
        float distanceToSlotCentre = Vector2.Distance(position, slotTransform.position);
        Debug.Log(distanceToSlotCentre);
        if (distanceToSlotCentre < slotColliderSize) 
            return true;
        return false;
    }

    public void Destroy()
    {
        if (itemIconInSlot)
            Destroy(itemIconInSlot.gameObject);

        Destroy(gameObject);
    }
}
