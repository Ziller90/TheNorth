using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    [SerializeField] int fieldOfView;
    [SerializeField] float maxDistanceToSee;
    [SerializeField] float maxDistanceToHear;
    [SerializeField] float viewPointOffset;

    List<Transform> unitsOnLocation = new List<Transform>();
    FactionMarker factionMarker;

    void Start()
    {
        unitsOnLocation = Links.instance.globalLists.unitsOnLocation;
        factionMarker = GetComponent<FactionMarker>();  
    }

    bool NoWallsOnVisionLine(Vector3 enemyPosition)
    {
        RaycastHit hitInfo;
        bool seeCollider = Physics.Raycast(transform.position + (enemyPosition - transform.position).normalized * viewPointOffset, (enemyPosition - transform.position), out hitInfo);
        if (seeCollider)
        {
            if (hitInfo.collider.gameObject.transform.position == enemyPosition)
            {
                return true;
            }
        }
        return false;
    }

    public Transform GetNearestEnemy()
    {
        List<Transform> noticedEnemies = new List<Transform>();
        foreach (Transform unit in unitsOnLocation)
        {
            if (unit.GetComponent<FactionMarker>().faction != factionMarker.faction && unit != gameObject.transform)
            {
                Vector3 fromGameObjectToEnemy = unit.position - transform.position;
                float distanceToEnemy = Vector3.Distance(transform.position, unit.position);

                if (distanceToEnemy < maxDistanceToSee && Vector3.Angle(transform.forward, fromGameObjectToEnemy) < (fieldOfView / 2) && (NoWallsOnVisionLine(unit.position)))
                {
                    noticedEnemies.Add(unit);
                }
                else if (distanceToEnemy < maxDistanceToHear)
                {
                    noticedEnemies.Add(unit);
                    continue;
                }
            }
        }
        if (noticedEnemies.Count != 0)
        {
            return ModelUtils.GetNearest(transform, noticedEnemies);
        }
        return null;
    }
}

