using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sensors : MonoBehaviour
{
    [SerializeField] Range visionRange;
    [SerializeField] Range hearingRange;

    List<Transform> noticedUnits = new();
    List<Transform> noticedEnemies = new();
    FactionMarker factionMarker;

    int terrainLayer = 7;
    int evniromentLayer = 9;
    int defaultLayer = 0;

    Transform nearestEnemy;
    public GameObject NearestEnemy => nearestEnemy ? nearestEnemy.gameObject : null;

    void Start()
    {
        factionMarker = GetComponent<FactionMarker>();  
    }

    void Update()
    {
        noticedUnits = GetNoticedUnits();
        noticedEnemies = GetEnemies(noticedUnits, factionMarker);
        nearestEnemy = GetNearestEnemy(noticedEnemies);   
    }

    public List<Transform> GetEnemies(List<Transform> units, FactionMarker factionMarker) 
    {
        return units.Where(x => x.GetComponent<FactionMarker>() && x.GetComponent<FactionMarker>().Faction != factionMarker.Faction).ToList();
    }

    public List<Transform> GetNoticedUnits()
    {
        LayerMask terrainLayerMask = 1 << terrainLayer;
        LayerMask evniromentLayerMask = 1 << evniromentLayer;
        LayerMask defaultLayerMask = 1 << defaultLayer;
        LayerMask obstaclesMask = terrainLayerMask | evniromentLayerMask | defaultLayerMask;

        var seenObjects = GetSeenObjectsInRange(Game.ActorsAccessModel.Units, visionRange, obstaclesMask);
        var heardObjects = GetObjectsInRange(Game.ActorsAccessModel.Units, hearingRange);

        var noticedUnits = seenObjects.Union(heardObjects).ToList();
        return noticedUnits;
    }

    public Transform GetNearestEnemy(List<Transform> units)
    {
        return ModelUtils.GetNearest(transform, units);
    }

    public List<Transform> GetObjectsInRange(IReadOnlyList<Transform> objects, Range range)
    {
        return objects.Where(x => x != transform && range.IsPointInRange(x.position)).ToList();    
    }

    public List<Transform> GetSeenObjectsInRange(IReadOnlyList<Transform> objects, Range range, LayerMask obstaclesMask)
    {
        return GetObjectsInRange(objects, range).Where(x => !ModelUtils.HaveObstaclesOnRaycast(range.transform.position, x.position, obstaclesMask)).ToList();    
    }
}

