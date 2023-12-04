using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractablesLocator : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] ActionManager actionManager;
    HumanoidInventoryContainer inventory;

    List<InteractableObject> interactablesOnLocation;
    InteractableObject nearestInteractable = null;

    public delegate void ContainerOpened(ContainerBody containerBody);
    public event ContainerOpened containerOpenedEvent;
    void Start()
    {
        inventory = GetComponent<HumanoidInventoryContainer>();  
        actionManager.onInteractPressed += Interact;
        interactablesOnLocation = Links.instance.globalLists.interactablesOnLocation;
    }
    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
    void Update()
    {
        InteractableObject newNearestInteractable = null;
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
            if (nearestInteractable.GetComponent<Item>())
            {
                var item = nearestInteractable.GetComponent<Item>();
                inventory.TryPickUpItem(item);
            }

            if (nearestInteractable.GetComponent<ContainerBody>())
            {
                var containerBody = nearestInteractable.GetComponent<ContainerBody>();
                containerOpenedEvent?.Invoke(containerBody);
            }
        }
    }
}
