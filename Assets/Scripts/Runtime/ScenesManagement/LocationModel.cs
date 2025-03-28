using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocationModel : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] string locationName;
    [SerializeField] string locationDescription;
    [SerializeField] int locationID;
    [SerializeField] LightingPreset lightingPreset;
    [SerializeField] AudioClip locationDefaultTheme;

    public string Name => locationName;
    public string Description => locationDescription;
    public int LocationID => locationID;
    public LightingPreset LightingPreset => lightingPreset;
    public AudioClip LocationDefaultTheme => locationDefaultTheme;

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
    }

    public Transform GetSpawnPointByIndex(int index)
    {
        return spawnPoints[index];
    }
}
