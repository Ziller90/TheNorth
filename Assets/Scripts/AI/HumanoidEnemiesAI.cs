using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Chase,
    BlindChase,
    Patrol,
    Idle,
    Attack,
    SearchEnemy
}
public class HumanoidEnemiesAI : MonoBehaviour
{
    [SerializeField] Sensors sensors;
    [SerializeField] States AIState;
    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToLastEnemyPosition;
    [SerializeField] bool hasPatrolRoute;
    [SerializeField] RouteManager routeManager;
    [SerializeField] GameObject debugPoint;
    [SerializeField] ActionManager actionManager;

    [SerializeField] float searchingRadius;
    [SerializeField] float enemySearchingTime;
    [SerializeField] int searhingCornersNumber;


    bool searchEnemy;
    Vector3 enemyPosition;
    Vector3 lastEnemyPosition;
    bool searhingRouteGenerated;
    Transform currentEnemy;

    void Update()
    {
        SetAIState();
        switch (AIState)
        {
            case States.Attack:
                navigationManager.Stand();
                actionManager.rightHandWeaponUsing = true;
                break;
            case States.Chase:
                navigationManager.MoveToTarget(currentEnemy.position, MovingMode.Run);
                actionManager.rightHandWeaponUsing = false;
                break;
            case States.BlindChase:
                navigationManager.MoveToTarget(enemyPosition, MovingMode.Run);
                actionManager.rightHandWeaponUsing = false;
                break;
            case States.Patrol:
                routeManager.SetDefaultPatrolRoute();
                routeManager.MoveOnRoute(MovingMode.Walk);
                actionManager.rightHandWeaponUsing = false;
                break;
            case States.Idle:
                navigationManager.Stand();
                actionManager.rightHandWeaponUsing = false;
                break;
            case States.SearchEnemy:
                if (searhingRouteGenerated == false)
                {
                    routeManager.SetNewRoute(GenerateSearchingRoute(lastEnemyPosition), 0);
                    searhingRouteGenerated = true;
                }
                routeManager.MoveOnRoute(MovingMode.Walk);
                actionManager.rightHandWeaponUsing = false;
                break;
        }
    }
    void SetAIState()
    {
        currentEnemy = sensors.GetNearestEnemy();
        if (currentEnemy != null)
        {
            enemyPosition = currentEnemy.position;
            searhingRouteGenerated = false;
            if (Vector3.Distance(transform.position, currentEnemy.position) < distanceToAttack)
            {
                AIState = States.Attack;
            }
            else
            {
                AIState = States.Chase;
            }
        }
        else if (enemyPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, enemyPosition) < distanceToLastEnemyPosition)
            {
                lastEnemyPosition = enemyPosition;
                searchEnemy = true;
                StartCoroutine("SearchingEnemyTimer");
                enemyPosition = Vector3.zero;
            }
            else
            {
                AIState = States.BlindChase;
            }
        }
        else if (searchEnemy == true)
        {
            AIState = States.SearchEnemy;
        }
        else if (hasPatrolRoute)
        {
            AIState = States.Patrol; 
        }
        else
        {
            AIState = States.Idle;
        }
    }
    IEnumerator SearchingEnemyTimer()
    {
        yield return new WaitForSeconds(enemySearchingTime);
        Debug.Log("StopSearchiing");
        searchEnemy = false;
        searhingRouteGenerated = false;
    }
    public Vector3[] GenerateSearchingRoute(Vector3 LastSeenPosition)
    {
        Vector3[] newRoute = new Vector3[searhingCornersNumber];
        for (int i = 0; i < searhingCornersNumber; i++)
        {
            Vector3 newCorner = LastSeenPosition + Random.insideUnitSphere * searchingRadius;
            newCorner.y = 1.3f;
            NavMeshHit hit;
            NavMesh.SamplePosition(newCorner, out hit, 2, 1);
            newCorner = hit.position;
            newRoute[i] = newCorner;
            Instantiate(debugPoint, newCorner, Quaternion.identity);
        }
        return newRoute;
    }
}
