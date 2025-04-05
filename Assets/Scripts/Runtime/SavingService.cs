using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeUp.Core;

public class SavingService : MonoBehaviour
{
    [AutoSerialize(0)] SerializedGameObjectBin savedPlayer;

    List<SavedLocation> locationSaves = new();

    public SerializedGameObjectBin SavedPlayer => savedPlayer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class SavedLocation
    {
        [AutoSerialize(0)] public string locationName;
        [AutoSerialize(1)] public int locationId;
        [AutoSerialize(2)] public List<SerializedGameObjectBin> serializedGameObjectsBin;
    }

    public void SaveLocation()
    {
        var serializedLocation = SerializeLocation(Game.LocationLoader.LoadedLocationModel);

        for (int i = 0; i < locationSaves.Count; i++)
        {
            if (locationSaves[i].locationId == serializedLocation.locationId)
            {
                locationSaves[i] = serializedLocation;
                return;
            }
        }
        locationSaves.Add(serializedLocation);
    }

    public void RestoreLocation(LocationModel location)
    {
        foreach(var locationSave in locationSaves)
        {
            if (locationSave.locationId == location.LocationID)
            {
                var restoreProcess = CreateRestoreProcess(location, locationSave);
                AutoSerializeTool.RestoreScene(restoreProcess);
            }
        }
    }

    RestoreProcess CreateRestoreProcess(LocationModel location, SavedLocation savedLocation)
    {
        List<SerializedGameObjectBin> serializedGameObjectsBin;

        int version = 1;
        serializedGameObjectsBin = savedLocation.serializedGameObjectsBin;
        var uniqueIds = FindObjectsOfType<UniqueId>();
        var restoreProcess = new RestoreProcess(serializedGameObjectsBin, uniqueIds, location.transform, version)
        {
            spawn = Game.ActorsAccessModel.SpawnObject,
            destroy = Game.ActorsAccessModel.DestroyObject
        };
        return restoreProcess;
    }

    public SavedLocation SerializeLocation(LocationModel locationToSave)
    {
        var savedLocation = new SavedLocation();
        savedLocation.locationId = locationToSave.LocationID;
        savedLocation.locationName = locationToSave.Name;

        var uniqueIds = FindObjectsOfType<UniqueId>();
        savedLocation.serializedGameObjectsBin = AutoSerializeTool.SerializeGameObjects(uniqueIds);
        return savedLocation;
    }

    public void SavePlayer()
    {
        var player = Game.GameSceneInitializer.Player;
        savedPlayer = AutoSerializeTool.SerializeBin(player);
    }

    public GameObject RestorePlayer()
    {
        var uniqueIds = FindObjectsOfType<UniqueId>();
        var restoreProcess = new RestoreProcess(new List<SerializedGameObjectBin> {savedPlayer}, uniqueIds)
        {
            spawn = Game.ActorsAccessModel.SpawnObject,
            destroy = Game.ActorsAccessModel.DestroyObject
        };

        return AutoSerializeTool.RestoreObjects(restoreProcess)[0];
    }
}
