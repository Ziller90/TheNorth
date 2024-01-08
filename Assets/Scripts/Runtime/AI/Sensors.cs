using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sensors : MonoBehaviour
{
    [SerializeField] Transform visionPosition;
    [SerializeField] float FOVAngle;
    [SerializeField] float maxDistanceToSee;
    [SerializeField] float maxDistanceToHear;
    [SerializeField] float viewPointOffset;

    List<Transform> unitsOnLocation = new List<Transform>();
    FactionMarker factionMarker;

    int terrainLayer = 7;
    int evniromentLayer = 9;

    void Start()
    {
        unitsOnLocation = Links.instance.globalLists.unitsOnLocation;
        unitsOnLocation.Remove(transform);
        factionMarker = GetComponent<FactionMarker>();  
    }

    public Transform GetNearestEnemy()
    {
        LayerMask terrainLayerMask = 1 << terrainLayer;
        LayerMask evniromentLayerMask = 1 << evniromentLayer;
        LayerMask obstaclesMask = terrainLayerMask | evniromentLayerMask;

        var seenObjects = ModelUtils.FindObjectsInFOV(visionPosition, maxDistanceToSee, FOVAngle, unitsOnLocation, obstaclesMask);
        var heardObjects = ModelUtils.FindObjectsInRadius(visionPosition, maxDistanceToSee, unitsOnLocation);

        var noticedObjects = seenObjects.Union(heardObjects).ToList();

        foreach (var obj in noticedObjects) 
        {
            if (obj.GetComponent<FactionMarker>() != null && obj.GetComponent<FactionMarker>().faction != factionMarker.faction)
                noticedObjects.Add(obj);
        }

        if (noticedObjects.Count != 0)
            return ModelUtils.GetNearest(transform, noticedObjects);

        return null;
    }
}

