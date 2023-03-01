using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> locationsList;
    [SerializeField] int locationToLoadIndex;
    [SerializeField] bool loadLocationFromGlobalMap;
    public void LoadLocation()
    {
        GameObject location;
        if (loadLocationFromGlobalMap)
        {
            location = Instantiate(locationsList[GameSceneLauncher.LocationToLoad], gameObject.transform);
        }
        else
        {
            location = Instantiate(locationsList[locationToLoadIndex], gameObject.transform);
        }
        Links.instance.locationSettings = location.GetComponent<LocationSettings>();
    }
}
