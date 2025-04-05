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
        if (ScenesLauncher.LocationToLoad != null)
        {
            location = LoadLocation(ScenesLauncher.LocationToLoad);
        }
        else if (testLocationToLoad != null)
        {
            location = LoadLocation(testLocationToLoad);
        }
        else
        {
            Debug.LogError("Error: No location to Load!");
            return;
        }
        loadedLocationModel = location;
    }

    public LocationModel LoadLocation(GameObject locationPrefab)
    {
        var loadedLocation = Instantiate(locationPrefab, gameObject.transform).GetComponent<LocationModel>();
        Game.LightingManager.SetLightingPreset(loadedLocation.LightingPreset);

        if (loadedLocation.LocationDefaultTheme)
            Game.MusicService.SetMusicTrack(loadedLocation.LocationDefaultTheme);

        return loadedLocation;  
    }

    public Transform GetSpawnPoint()
    {
        var spawnPoint = ScenesLauncher.spawnPoint != -1 ? loadedLocationModel.GetSpawnPointByIndex(ScenesLauncher.spawnPoint) : loadedLocationModel.GetRandomSpawnPoint();
        ScenesLauncher.spawnPoint = -1;
        return spawnPoint;
    }
}
