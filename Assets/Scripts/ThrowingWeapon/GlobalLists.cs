using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLists : MonoBehaviour
{
    public List<Transform> creaturesOnLocation;
    public List<Transform> itemsOnLocation;

    public void AddToItemsOnLocation(Transform item)
    {
        itemsOnLocation.Add(item);
    }
    public void RemoveFromItemsOnLocation(Transform item)
    {
        if (itemsOnLocation.Contains(item))
            itemsOnLocation.Remove(item);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
