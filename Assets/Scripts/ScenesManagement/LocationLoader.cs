using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> locationsList;
    [SerializeField] int locationToLoadIndex;
    [SerializeField] bool loadLocationFromGlobalMap;
    void Start()
    {
        if (loadLocationFromGlobalMap)
        {
            Instantiate(locationsList[GlobalMap.LocationToLoad], gameObject.transform);
        }
        else
        {
            Instantiate(locationsList[locationToLoadIndex], gameObject.transform);
        }
    }
}
