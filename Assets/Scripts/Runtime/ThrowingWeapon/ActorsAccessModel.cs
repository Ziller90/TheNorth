using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SiegeUp.Core;

[DefaultExecutionOrder(ApplicationServices.ExecutionOrder + 10)]
public class ActorsAccessModel : MonoBehaviour
{
    public delegate void UnitUpdated(Transform unit);
    public delegate void InteractableObjectUpdated(InteractableObject unit);

    public event UnitUpdated unitRegisterd;
    public event UnitUpdated unitUnregisterd;

    public event InteractableObjectUpdated interactableObjectRegistered;
    public event InteractableObjectUpdated interactableObjectUnegistered;

    List<Transform> units = new();
    List<InteractableObject> interactableObjects = new();

    public IReadOnlyList<Transform> Units => units;
    public IReadOnlyList<InteractableObject> InteractableObjects => interactableObjects;

    public void RegisterUnit(Transform unit)
    {
        if (!units.Contains(unit))
        {
            units.Add(unit);
            unitRegisterd?.Invoke(unit);    
        }
    }

    public void RegisterInteractableObject(InteractableObject interactableObject)
    {
        if (!interactableObjects.Contains(interactableObject))
        {
            interactableObjects.Add(interactableObject);
            interactableObjectRegistered?.Invoke(interactableObject);
        }
    }

    public void RemoveUnitOnLocation(Transform unit)
    {
        units.Remove(unit);
        unitUnregisterd?.Invoke(unit);  
    }

    public void RemoveInteractableOnLocation(InteractableObject interactableObject)
    {
        interactableObjects.Remove(interactableObject);
        interactableObjectUnegistered?.InvokeSafe(interactableObject);
    }
}
