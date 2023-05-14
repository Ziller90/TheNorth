using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollector : MonoBehaviour
{
    [SerializeField] float pickUpRange;
    [SerializeField] Container playerInventoryContainer;
    [SerializeField] Transform itemsDropPosition;

    [SerializeField] ActionManager actionManager;
    List<Transform> itemsOnLocation;
    bool hasObjectInRange;
    Transform nearestItem;
    void Start()
    {
        actionManager.OnPickUpItemPressed += AddItemToContainer;
        itemsOnLocation = Links.instance.globalLists.itemsOnLocation;
    }
    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
    void Update()
    {
        bool ojectInRange = false;
        float minDistanceToItem = pickUpRange;

        foreach (Transform item in itemsOnLocation)
        {
            float distance = (Vector3.Distance(item.position, gameObject.transform.position));
            if (distance < pickUpRange)
            {
                ojectInRange = true;
                if (minDistanceToItem > distance)
                {
                    nearestItem = item;
                    minDistanceToItem = distance;
                }
            }
        }
        if (hasObjectInRange == false)
        {
            nearestItem = null;
        }
        hasObjectInRange = ojectInRange;
    }
    public void AddItemToContainer() 
    {
        if (hasObjectInRange && playerInventoryContainer.HasFreeSpace)
        {
            playerInventoryContainer.AddNewItem(nearestItem.gameObject.GetComponent<Item>());
            Links.instance.globalLists.RemoveFromItemsOnLocation(nearestItem);
            nearestItem.transform.position = new Vector3(-1000, -1000, -1000);
            nearestItem.GetComponent<Rigidbody>().isKinematic = true;
            nearestItem.GetComponent<Item>().SetItemState(true);
        }
    }
    public void Drop(Item item)
    {
        Links.instance.globalLists.AddToItemsOnLocation(item.transform);
        item.SetItemState(false);
        item.transform.position = itemsDropPosition.position;
        item.GetComponent<Rigidbody>().isKinematic = false;
    }
}
