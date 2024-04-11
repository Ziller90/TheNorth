using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeUp.Core;

public class SavingService : MonoBehaviour
{
    List<SavedLocation> locationSaves = new();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class SavedLocation
    {
        [AutoSerialize(0)]
        public string locationName;

        [AutoSerialize(1)]
        public int locationId;

        [AutoSerialize(2)]
        public List<SerializedGameObjectBin> serializedGameObjectsBin;
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

    public SavedLocation SerializeLocation(LocationModel locationToSave)
    {
        var savedLocation = new SavedLocation();
        savedLocation.locationId = locationToSave.LocationID;
        savedLocation.locationName = locationToSave.Name;

        var uniqueIds = FindObjectsOfType<UniqueId>();
        savedLocation.serializedGameObjectsBin = AutoSerializeTool.SerializeGameObjects(uniqueIds);
        return savedLocation;
    }
}
