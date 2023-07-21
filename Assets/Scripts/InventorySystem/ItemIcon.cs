using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IDragHandler,  IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image icon;
    [SerializeField] float timeToShowDesctiption;
    [SerializeField] ItemDescriptionPanel descriptionPanelPrefab;

    ItemDescriptionPanel descriptionPanel;
    ItemsViewManager itemsViewManager;
    Item item;
    public Item Item => item;
    bool isDragged = false;
    bool isHold = false;

    public void SetItem(Item item, ItemsViewManager itemsViewManager)
    {
        this.item = item;
        this.itemsViewManager = itemsViewManager;
        icon.sprite = item.Info.Icon;
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
        MoveItemToSlot(newSlot);

        itemsViewManager.SetSelectedIcon(this);
        isDragged = false;
        isHold = false;
        StopAllCoroutines();
        DestroyDescriptionPanel();
    }
    public void MoveItemToSlot(Slot newSlot)
    {
        var currentSlot = itemsViewManager.GetItemIconSlot(this);
        if (newSlot == null || newSlot == currentSlot)
        {
            currentSlot.StartCoroutine(currentSlot.SetIconInSlotPosition(this));
        }
        else if (newSlot.ItemIcon == null)
        {
            currentSlot.RemoveIcon();
            newSlot.InsertIcon(this, false);
        }
        else
        {
            currentSlot.RemoveIcon();
            currentSlot.InsertIcon(newSlot.ItemIcon, false);
            newSlot.RemoveIcon();
            newSlot.InsertIcon(this, false);
        }
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
        descriptionPanel.transform.position = itemIcon.transform.position + new Vector3(120, -120, 0);
        descriptionPanel.SetItemData(itemIcon.Item);
    }
    public void DestroyDescriptionPanel()
    {
        if (descriptionPanel)
            Destroy(descriptionPanel.gameObject);
    }
}
