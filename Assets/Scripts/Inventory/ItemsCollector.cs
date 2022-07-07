using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollector : MonoBehaviour
{
    public List<Transform> itemsOnLocation;
    public float minDistance;
    public bool hasObjectInRange;
    public Transform nearestItem;
    public Container playerInventoryContainer;
    public Transform itemsDropPosition;

    void Start()
    {
        itemsOnLocation = LinksContainer.instance.globalLists.itemsOnLocation;
    }
    void Update()
    {
        bool ojectInRange = false;
        float minDistanceToItem = minDistance;
        foreach (Transform item in itemsOnLocation)
        {
            float distance = (Vector3.Distance(item.position, gameObject.transform.position));
            if (distance < minDistance)
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
        if (hasObjectInRange)
        {
            playerInventoryContainer.AddNewItem(nearestItem.gameObject.GetComponent<Item>());
            LinksContainer.instance.globalLists.RemoveFromItemsOnLocation(nearestItem);
            nearestItem.transform.position = new Vector3(-1000, -1000, -1000);
            nearestItem.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void Drop(Item item)
    {
        playerInventoryContainer.RemoveItem(item);
        LinksContainer.instance.globalLists.AddToItemsOnLocation(item.transform);
        item.transform.position = itemsDropPosition.position;
        item.GetComponent<Rigidbody>().isKinematic = false;
    }
}
