using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalLists : MonoBehaviour
{
    public List<Transform> unitsOnLocation = new();
    public List<InteractableObject> interactablesOnLocation = new();

    public void AddUnitOnLocation(Transform unit)
    {
        if (!unitsOnLocation.Contains(unit))
            unitsOnLocation.Add(unit);
    }

    public void RemoveUnitOnLocation(Transform unit)
    {
        if (unitsOnLocation.Contains(unit))
            unitsOnLocation.Remove(unit);
    }

    public void AddInteractableOnLocation(InteractableObject item)
    {
        if (!interactablesOnLocation.Contains(item))
            interactablesOnLocation.Add(item);
    }

    public void RemoveInteractableOnLocation(InteractableObject item)
    {
        if (interactablesOnLocation.Contains(item))
            interactablesOnLocation.Remove(item);
    }
}
