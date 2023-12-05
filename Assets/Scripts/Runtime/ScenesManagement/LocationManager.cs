using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] string locationName;
    [SerializeField] string locationDescription;

    public string Name => locationName;
    public string Description => locationDescription;
    void Start()
    {
        Links.instance.locationSettings = this;
    }
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
