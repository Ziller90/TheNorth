using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public List<Transform> creaturesOnLocation;
    public Transform currentEnemy;
    public FractionMarker fractionMarker;
    public int fieldOfView;
    public float minDistanceToSee;
    public GameObject ThisCreature;
    public float viewPointOffset;

    private bool noWalls;
    private Vector3 enemyPosition;
    void Start()
    {
        creaturesOnLocation = LinksContainer.instance.globalLists.creaturesOnLocation;
    }
    void Update() 
    {
        currentEnemy = GetNearestEnemy();
    }
    bool NoWallsOnVisionLine(Vector3 enemyPosition)
    {
        this.enemyPosition = enemyPosition;
        RaycastHit hitInfo;
        bool seeCollider = Physics.Raycast(transform.position + (enemyPosition - transform.position).normalized * viewPointOffset, (enemyPosition - transform.position), out hitInfo);
        if (seeCollider)
        {
            Debug.Log(hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.transform.position == enemyPosition)
            {
                return true;
            }
        }
        return false;
    }
    public Transform GetNearestEnemy()
    {
        List<Transform> visibleEnemies = new List<Transform>();
        for (int i = 0; i < creaturesOnLocation.Count; i++)
        {
            if (creaturesOnLocation[i].GetComponent<FractionMarker>().creatureFraction != fractionMarker.creatureFraction && creaturesOnLocation[i] != ThisCreature.transform)
            {
                Vector3 fromGameObjectToEnemy = creaturesOnLocation[i].position - transform.position;
                float distanceToEnemy = Vector3.Distance(transform.position, creaturesOnLocation[i].position);

                noWalls = NoWallsOnVisionLine(creaturesOnLocation[i].position);
                if (distanceToEnemy < minDistanceToSee && Vector3.Angle(transform.parent.forward, fromGameObjectToEnemy) < (fieldOfView / 2) && (NoWallsOnVisionLine(creaturesOnLocation[i].position)))
                {
                    visibleEnemies.Add(creaturesOnLocation[i]);
                }
            }
        }
        if (visibleEnemies != null)
        {
            return Utils.GetNearest(transform, visibleEnemies);
        }
        return null;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + (enemyPosition - transform.position).normalized * viewPointOffset, (enemyPosition - transform.position));
    }
}

