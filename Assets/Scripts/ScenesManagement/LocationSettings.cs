using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSettings : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] string locationName;
    [SerializeField] string locationDescription;

    void Start()
    {
        Links.instance.locationSettings = this;
    }
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
