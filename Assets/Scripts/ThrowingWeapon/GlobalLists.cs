using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLists : MonoBehaviour
{
    public List<Transform> creaturesOnLocation;
    public List<InteractableObject> interactablesOnLocation;

    public void AddInteractableOnLocation(InteractableObject item)
    {
        interactablesOnLocation.Add(item);
    }
    public void RemoveInteractableOnLocation(InteractableObject item)
    {
        if (interactablesOnLocation.Contains(item))
            interactablesOnLocation.Remove(item);
    }
}
