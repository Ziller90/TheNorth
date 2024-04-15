using SiegeUp.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    Dictionary<Guid, UniqueId> objectsMap = new();

    public IReadOnlyDictionary<Guid, UniqueId> ObjectsMap => objectsMap;
    public IReadOnlyList<Transform> Units => units;
    public IReadOnlyList<InteractableObject> InteractableObjects => interactableObjects;

    private void Awake()
    {
        var uniqueIds = FindObjectsOfType<UniqueId>();
        foreach (var uniqueId in uniqueIds)
            objectsMap.Add(uniqueId.GetGuid(), uniqueId);
    }

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

    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation = default, Guid? newUniqueId = null)
    {
        GameObject obj = null;
        obj = Instantiate(prefab, position, rotation != default ? rotation : Quaternion.identity);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
