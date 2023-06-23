using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public class InteractablesLocator : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] Container inventoryContainer;
    [SerializeField] Transform itemsDropPosition;

    [SerializeField] ActionManager actionManager;
    List<Interactable> interactablesOnLocation;
    Interactable nearestInteractable = null;
    void Start()
    {
        actionManager.OnInteractPressed += Interact;
        interactablesOnLocation = Links.instance.globalLists.interactablesOnLocation;
    }
    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
    void Update()
    {
        Interactable newNearestInteractable = null;
        float minDistance = 1000000;
        foreach (var interactable in interactablesOnLocation)
        {
            float distance = (Vector3.Distance(interactable.transform.position, gameObject.transform.position));

            if (distance < interactionRange)
            {
                if (distance < minDistance)
                {
                    newNearestInteractable = interactable;
                    minDistance = distance;
                }
            }
        }

        if (nearestInteractable != newNearestInteractable)
        {
            if (nearestInteractable)
                nearestInteractable.SetHighlighted(false);
            if (newNearestInteractable)
                newNearestInteractable.SetHighlighted(true);
            nearestInteractable = newNearestInteractable;
        }
    }
    public void Interact()
    {
        if (nearestInteractable)
        {
            if (nearestInteractable.GetComponent<Item>() && inventoryContainer.HasFreeSpace)
            {
                var item = nearestInteractable.GetComponent<Item>();
                Links.instance.globalLists.RemoveInteractableOnLocation(nearestInteractable);
                AddItemToContainer(item);
            }
            if (nearestInteractable.GetComponent<ContainerBody>())
            {
                var container = nearestInteractable.GetComponent<ContainerBody>();
                container.OpenContainer();
            }
        }
    }
    public void AddItemToContainer(Item item) 
    {
        inventoryContainer.AddNewItem(item);
        item.transform.position = new Vector3(-1000, -1000, -1000);
        item.SetItemInInventory(true);
    }
    public void Drop(Item item)
    {
        Links.instance.globalLists.AddInteractableOnLocation(item.GetComponent<Interactable>());
        item.SetItemInInventory(false);
        item.transform.position = itemsDropPosition.position;
    }
}
