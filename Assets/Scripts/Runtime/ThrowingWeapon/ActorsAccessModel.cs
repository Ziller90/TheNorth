using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorsAccessModel : MonoBehaviour
{
    List<Transform> units = new();
    List<InteractableObject> interactableObjects = new();

    public IReadOnlyList<Transform> Units => units;
    public IReadOnlyList<InteractableObject> InteractableObjects => interactableObjects;

    public void AddUnitOnLocation(Transform unit)
    {
        if (!units.Contains(unit))
            units.Add(unit);
    }

    public void AddInteractableOnLocation(InteractableObject item)
    {
        if (!interactableObjects.Contains(item))
            interactableObjects.Add(item);
    }

    public void RemoveUnitOnLocation(Transform unit) => units.Remove(unit);
    public void RemoveInteractableOnLocation(InteractableObject item) => interactableObjects.Remove(item);
}
