using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] GameObject testLocationToLoad;

    LocationModel loadedLocationModel;
    public LocationModel LoadedLocationModel => loadedLocationModel;    

    public void LoadLocation()
    {
        LocationModel location;
        if (GameSceneLauncher.LocationToLoad != null)
        {
            location = Instantiate(GameSceneLauncher.LocationToLoad, gameObject.transform).GetComponent<LocationModel>();
        }
        else if (testLocationToLoad != null)
        {
            location = Instantiate(testLocationToLoad, gameObject.transform).GetComponent<LocationModel>();
        }
        else
        {
            Debug.LogError("Error: No location to Load!");
            return;
        }
        loadedLocationModel = location;
    }
}
