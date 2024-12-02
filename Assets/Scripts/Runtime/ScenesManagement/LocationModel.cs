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
    [SerializeField] bool indoorsSpawnPoint;

    public string Name => locationName;
    public string Description => locationDescription;
    public int LocationID => locationID;
    public bool IsIndoorsSpawnPoint => indoorsSpawnPoint;

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
    }
}
