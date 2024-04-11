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
    

    public string Name => locationName;
    public string Description => locationDescription;
    public int LocationID => locationID;

    void Start()
    {
        Links.instance.locationModel = this;
    }
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
    }
}
