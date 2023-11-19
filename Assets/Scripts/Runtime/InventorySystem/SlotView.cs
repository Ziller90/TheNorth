using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    Slot slot;
    ItemIcon itemIconInSlot;
    RectTransform slotTransform;
    
    public Slot Slot => slot;
    public bool IsEmpty => itemIconInSlot == null;
    public ItemIcon ItemIcon => itemIconInSlot;

    public UnityEvent<ItemIcon, SlotView> iconInsertedEvent;
    public UnityEvent<ItemIcon, SlotView> iconRemovedEvent;

    [SerializeField] ItemIcon itemIconPrefab;
    [SerializeField] bool slotIsRhombus;
    [SerializeField] Image iconShadow;
    [SerializeField] Image blockImage;
    [SerializeField] Image selectionMarker;

    [SerializeField] SlotType slotType;

    public SlotType SlotType => slotType;
    public bool IsBlocked => isBlocked;

    bool isBlocked;

    private void OnEnable()
    {
        slotTransform = GetComponent<RectTransform>();
    }

    public void SetBlocked(bool isBlocked, Sprite blockImage = null)
    {
        this.isBlocked = isBlocked;
        this.blockImage.sprite = blockImage;
        this.blockImage.gameObject.SetActive(isBlocked);
    }

    public void SetSlot(Slot slot, ItemsViewManager itemsViewManager)
    {
        this.slot = slot;
        if (!slot.isEmpty)
            InstantiateItemIcon(itemsViewManager);
    }

    public void InstantiateItemIcon(ItemsViewManager itemsViewManager)
    {
        var newIcon = Instantiate(itemIconPrefab, itemsViewManager.CommonIconsContainer);
        newIcon.SetItemStack(slot.ItemStack, itemsViewManager);
        InsertIcon(newIcon, true);
    }

    public void InsertIcon(ItemIcon icon, bool initializing)
    {
        if (!initializing)
            iconInsertedEvent?.Invoke(icon, this);
        itemIconInSlot = icon;
        SetIconInSlotPosition();
    }

    public void SetIconInSlotPosition()
    {
        itemIconInSlot.transform.parent = slotTransform;
        var itemIconRectTransform = itemIconInSlot.GetComponent<RectTransform>();
        float width = itemIconRectTransform.rect.width;
        itemIconRectTransform.anchoredPosition= new Vector3(width/2, -width/2, 0);
    }

    public void PullOutIcon()
    {
        iconRemovedEvent?.Invoke(itemIconInSlot, this);
        itemIconInSlot = null;
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
            if (ModelUtils.isInRhombus(position, slotTransform.position, rhombusSize, rhombusSize))
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
        iconShadow.sprite = itemIcon.ItemStack.Item.Icon;
    }

    public void HideItemShadow()
    {
        iconShadow.gameObject.SetActive(false);
    }

    public void Destroy()
    {
        if (itemIconInSlot)
            Destroy(itemIconInSlot.gameObject);
        Destroy(gameObject);
    }

    public void DestroyItemIcon()
    {
        if (itemIconInSlot)
            Destroy(itemIconInSlot.gameObject);
    }

    public void DestroyWithPullOut()
    {
        var itemIconTemp = itemIconInSlot;
        PullOutIcon();
        if (itemIconTemp)
            Destroy(itemIconTemp.gameObject);
    }

    public bool IsSuitableSlotType(SlotType itemSuitableSlotType)
    {
        if (slotType != SlotType.None)
        {
            if (itemSuitableSlotType == SlotType.BothHanded)
            {
                if (slotType != SlotType.MainWeapon && slotType != SlotType.SecondaryWeapon)
                    return false;
            }
            else if (itemSuitableSlotType == SlotType.TwoHanded)
            {
                if (slotType != SlotType.MainWeapon)
                    return false;
            }
            else if (itemSuitableSlotType != slotType)
            {
                return false;
            }
        }
        return true;
    }
}
