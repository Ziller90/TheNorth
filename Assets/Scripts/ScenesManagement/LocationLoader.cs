using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] GameObject testLocationToLoad;
    public void LoadLocation()
    {
        GameObject location;
        if (GameSceneLauncher.LocationToLoad != null)
        {
            location = Instantiate(GameSceneLauncher.LocationToLoad, gameObject.transform);
        }
        else if (testLocationToLoad != null)
        {
            location = Instantiate(testLocationToLoad, gameObject.transform);
        }
        else
        {
            Debug.LogError("Error: No location to Load!");
            return;
        }
        Links.instance.locationSettings = location.GetComponent<LocationSettings>();
    }
}
