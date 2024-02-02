using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalLists : MonoBehaviour
{
    public List<Transform> unitsOnLocation;
    public List<InteractableObject> interactablesOnLocation;
    public List<Item> itemsPrefabs;

    public Item GetItemPrefabById(int id)
    {
        return itemsPrefabs.FirstOrDefault(i => i.Id == id);
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
