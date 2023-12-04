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
    public ItemIcon ItemIcon => itemIconInSlot;

    [SerializeField] ItemIcon itemIconPrefab;
    [SerializeField] bool slotIsRhombus;
    [SerializeField] Image iconShadow;
    [SerializeField] Image blockImage;
    [SerializeField] Image selectionMarker;

    void OnEnable()
    {
        slotTransform = GetComponent<RectTransform>();

        if (slot != null)
        {
            slot.blockStateUpdated -= SetBlockView;
            slot.blockStateUpdated += SetBlockView;
        }
    }

    void OnDisable()
    {
        if (slot != null)
        {
            slot.blockStateUpdated -= SetBlockView;
        }
    }

    void SetBlockView(bool isBlocked, Sprite blockSprite = null)
    {
        blockImage.gameObject.SetActive(isBlocked);
        blockImage.sprite = blockSprite;
    }

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
        slot.blockStateUpdated += SetBlockView;

        if (!slot.isEmpty)
            InstantiateItemIcon();
    }

    public void InstantiateItemIcon()
    {
        var newIcon = Instantiate(itemIconPrefab, slotTransform);
        newIcon.SetItemStack(slot.ItemStack);
        InsertIcon(newIcon);
    }

    public void InsertIcon(ItemIcon icon)
    {
        itemIconInSlot = icon;
        SetIconInSlotPosition();
    }

    public void SetIconInSlotPosition()
    {
        itemIconInSlot.transform.SetParent(slotTransform);
        var itemIconRectTransform = itemIconInSlot.GetComponent<RectTransform>();
        float width = itemIconRectTransform.rect.width;
        itemIconRectTransform.anchoredPosition = new Vector3(width / 2, -width / 2, 0);
    }

    public void PullOutIcon()
    {
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
            if (ModelUtils.IsInRhombus(position, slotTransform.position, rhombusSize, rhombusSize))
                return true;
        }
        return false;
    }

    public void SetSlotSelection(bool isSelected)
    {
        selectionMarker.gameObject.SetActive(isSelected);
        iconShadow.gameObject.SetActive(isSelected);

        if (isSelected && itemIconInSlot)
            iconShadow.sprite = itemIconInSlot.ItemStack.Item.Icon;
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

    public void PullOutAndDestroyItemIcon()
    {
        var itemIconTemp = itemIconInSlot;
        PullOutIcon();
        if (itemIconTemp)
            Destroy(itemIconTemp.gameObject);
    }
}
