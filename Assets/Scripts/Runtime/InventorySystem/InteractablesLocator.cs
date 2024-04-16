using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractablesLocator : MonoBehaviour
{
    [SerializeField] Range interactionRange;

    ActionManager actionManager;
    HumanoidInventoryContainer inventory;
    InteractableObject nearestInteractable = null;

    public delegate void ContainerOpened(ContainerBody containerBody);
    public event ContainerOpened containerOpenedEvent;

    void Start()
    {
        inventory = GetComponent<HumanoidInventoryContainer>();  
        actionManager = GetComponent<ActionManager>();
        actionManager.onInteractPressed += Interact;
    }

    void OnDisable()
    {
        if (nearestInteractable)
        {
            nearestInteractable.SetHighlighted(false);
            nearestInteractable = null;
        }
    }

    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }

    void Update()
    {
        InteractableObject newNearestInteractable = null;
        float minDistance = 1000000;
        foreach (var interactable in Game.ActorsAccessModel.InteractableObjects)
        {
            if (interactionRange.IsPointInRange(interactable.transform.position))
            {
                float distance = (Vector3.Distance(interactable.transform.position, gameObject.transform.position));
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
                containerBody.OpenContainer();
                containerOpenedEvent?.Invoke(containerBody);
            }

            if (nearestInteractable.GetComponent<DoorTeleporter>())
            {
                var containerBody = nearestInteractable.GetComponent<DoorTeleporter>();
                containerBody.OpenDoor(gameObject);
            }
        }
    }
}
