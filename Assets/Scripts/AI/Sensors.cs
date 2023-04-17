using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    [SerializeField] FactionMarker fractionMarker;
    [SerializeField] int fieldOfView;
    [SerializeField] float maxDistanceToSee;
    [SerializeField] float maxDistanceToHear;
    [SerializeField] GameObject ThisCreature;
    [SerializeField] float viewPointOffset;

    List<Transform> creaturesOnLocation;
    void Start()
    {
        creaturesOnLocation = Links.instance.globalLists.creaturesOnLocation;
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
        foreach (Transform creature in creaturesOnLocation)
        {
            if (creature.GetComponent<FactionMarker>().creatureFaction != fractionMarker.creatureFaction && creature != ThisCreature.transform)
            {
                Vector3 fromGameObjectToEnemy = creature.position - transform.position;
                float distanceToEnemy = Vector3.Distance(transform.position, creature.position);

                if (distanceToEnemy < maxDistanceToHear)
                {
                    noticedEnemies.Add(creature);
                    continue;
                }
                if (distanceToEnemy < maxDistanceToSee && Vector3.Angle(transform.parent.forward, fromGameObjectToEnemy) < (fieldOfView / 2) && (NoWallsOnVisionLine(creature.position)))
                {
                    noticedEnemies.Add(creature);
                }
            }
        }
        if (noticedEnemies.Count != 0)
        {
            return Utils.GetNearest(transform, noticedEnemies);
        }
        return null;
    }
}

