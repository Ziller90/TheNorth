using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public List<Transform> creaturesOnLocation;
    public FractionMarker fractionMarker;
    public int fieldOfView;
    public float maxDistanceToSee;
    public float maxDistanceToHear;
    public GameObject ThisCreature;
    public float viewPointOffset;
    void Start()
    {
        creaturesOnLocation = LinksContainer.instance.globalLists.creaturesOnLocation;
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
        for (int i = 0; i < creaturesOnLocation.Count; i++)
        {
            if (creaturesOnLocation[i].GetComponent<FractionMarker>().creatureFraction != fractionMarker.creatureFraction && creaturesOnLocation[i] != ThisCreature.transform)
            {
                Vector3 fromGameObjectToEnemy = creaturesOnLocation[i].position - transform.position;
                float distanceToEnemy = Vector3.Distance(transform.position, creaturesOnLocation[i].position);

                if (distanceToEnemy < maxDistanceToHear)
                {
                    noticedEnemies.Add(creaturesOnLocation[i]);
                    continue;
                }
                if (distanceToEnemy < maxDistanceToSee && Vector3.Angle(transform.parent.forward, fromGameObjectToEnemy) < (fieldOfView / 2) && (NoWallsOnVisionLine(creaturesOnLocation[i].position)))
                {
                    noticedEnemies.Add(creaturesOnLocation[i]);
                }
            }
        }
        if (noticedEnemies != null)
        {
            return Utils.GetNearest(transform, noticedEnemies);
        }
        return null;
    }
}

