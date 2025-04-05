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
        if (Game.SceneLauncherService.LocationToLoad != null)
        {
            location = LoadLocation(Game.SceneLauncherService.LocationToLoad);
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
        var spawnPoint = Game.SceneLauncherService.SpawnPoint != -1 ? loadedLocationModel.GetSpawnPointByIndex(Game.SceneLauncherService.SpawnPoint) : loadedLocationModel.GetRandomSpawnPoint();
        Game.SceneLauncherService.SetSpawnPoint(-1);
        return spawnPoint;
    }
}
