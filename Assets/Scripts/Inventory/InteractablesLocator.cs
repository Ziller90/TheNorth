using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public class InteractablesLocator : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] ActionManager actionManager;
    HumanoidInventory inventory;

    List<InteractableObject> interactablesOnLocation;
    InteractableObject nearestInteractable = null;
    void Start()
    {
        inventory = GetComponent<HumanoidInventory>();  
        actionManager.OnInteractPressed += Interact;
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
                Links.instance.globalLists.RemoveInteractableOnLocation(nearestInteractable);
                inventory.AddItem(item);
            }
            if (nearestInteractable.GetComponent<ContainerBody>())
            {
                var container = nearestInteractable.GetComponent<ContainerBody>();
                container.OpenContainer();
            }
        }
    }
}
