using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LocationModel : MonoBehaviour
{
    [SerializeField] string locationName;
    [SerializeField] string locationDescription;
    [SerializeField] int locationID;
    [SerializeField] LightingPreset lightingPreset;
    [SerializeField] AudioClip locationDefaultTheme;

    [SerializeField] Transform spawnPointsRoot;
    [SerializeField] List<Transform> spawnPoints;

    public string Name => locationName;
    public string Description => locationDescription;
    public int LocationID => locationID;
    public LightingPreset LightingPreset => lightingPreset;
    public AudioClip LocationDefaultTheme => locationDefaultTheme;


    [ContextMenu("RefreshSpawnPointsList")]
    public void RefreshSpawnPointsList()
    {
        spawnPoints = spawnPointsRoot.GetComponentsInChildren<Transform>().ToList();
        spawnPoints.Remove(spawnPointsRoot.transform);
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
    }

    public Transform GetSpawnPointByIndex(int index)
    {
        return spawnPoints[index];
    }
}
