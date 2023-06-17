using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemIcon itemIconInSlot;
    RectTransform slotTransform;
    bool selected;
    public bool isSelected => selected;
    public bool IsEmpty => itemIconInSlot == null;
    public ItemIcon ItemIcon => itemIconInSlot;

    public UnityEvent<ItemIcon, InventorySlot> iconInsertedEvent;
    public UnityEvent<ItemIcon, InventorySlot> iconRemovedEvent;

    [SerializeField] bool slotIsRhombus;
    [SerializeField] GameObject iconFootprintPrefab;
    [SerializeField] GameObject selectionMarkerPrefab;
    GameObject iconFootprint;
    GameObject selectionMarker;
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

    public void Destroy()
    {
        if (itemIconInSlot)
            Destroy(itemIconInSlot.gameObject);
        Destroy(gameObject);
    }
    public void InstantiateIconFootprint()
    {
        iconFootprint = Instantiate(iconFootprintPrefab, gameObject.transform);
        iconFootprint.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;

        iconFootprint.gameObject.GetComponent<Image>().sprite = itemIconInSlot.Item.ItemData.Icon;
        iconFootprint.transform.SetSiblingIndex(0);
    }
    public void DestroyIconFootprint()
    {
        Destroy(iconFootprint);
    }
    public void Select()
    {
        selected = true;
        selectionMarker = Instantiate(selectionMarkerPrefab, gameObject.transform);
        selectionMarker.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        selectionMarker.transform.SetSiblingIndex(0);
    }
    public void Deselect()
    {
        selected = false;
        Destroy(selectionMarker);
    }
}
