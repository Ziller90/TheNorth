using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLists : MonoBehaviour
{
    public List<Transform> creaturesOnLocation;
    public List<Interactable> interactablesOnLocation;

    public void AddInteractableOnLocation(Interactable item)
    {
        interactablesOnLocation.Add(item);
    }
    public void RemoveInteractableOnLocation(Interactable item)
    {
        if (interactablesOnLocation.Contains(item))
            interactablesOnLocation.Remove(item);
    }
}
