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
    public Sensors sensors;
    public States AIState;
    public AINavigationManager navigationManager;
    public Transform enemy;
    public float distanceToAttack;
    public float distanceToLastEnemyPosition;
    public bool hasPatrolRoute;
    public int timeForEnemySearching;
    public bool searchEnemy;
    public bool searhingRouteGenerated;
    public RouteManager routeManager;
    public float searchingRadius;

    public GameObject DebugSphere;
    public Transform currentEnemy;
    Vector3 enemyPosition;
    Vector3 lastEnemyPosition;
    public ActionManager actionManager;

    void Update()
    {
        SetAIState();
        switch (AIState)
        {
            case States.Attack:
                navigationManager.Stand();
                actionManager.isMeleeAttackActing = true;
                break;
            case States.Chase:
                navigationManager.MoveToTarget(currentEnemy.position, MovingMode.Run);
                actionManager.isMeleeAttackActing = false;
                break;
            case States.BlindChase:
                navigationManager.MoveToTarget(enemyPosition, MovingMode.Run);
                actionManager.isMeleeAttackActing = false;
                break;
            case States.Patrol:
                routeManager.SetDefaultPatrolRoute();
                routeManager.MoveOnRoute(MovingMode.Walk);
                actionManager.isMeleeAttackActing = false;
                break;
            case States.Idle:
                navigationManager.Stand();
                actionManager.isMeleeAttackActing = false;
                break;
            case States.SearchEnemy:
                if (searhingRouteGenerated == false)
                {
                    routeManager.SetNewRoute(GenerateSearchingRoute(lastEnemyPosition), 0);
                    searhingRouteGenerated = true;
                }
                routeManager.MoveOnRoute(MovingMode.Walk);
                actionManager.isMeleeAttackActing = false;
                break;
        }
    }
    void SetAIState()
    {
        currentEnemy = sensors.GetNearestEnemy();
        if (currentEnemy != null)
        {
            enemyPosition = currentEnemy.position;
        }

        if (currentEnemy != null)
        {
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
        yield return new WaitForSeconds(20);
        Debug.Log("StopSearchiing");
        searchEnemy = false;
        searhingRouteGenerated = false;
    }
    public Vector3[] GenerateSearchingRoute(Vector3 LastSeenPosition)
    {
        Vector3[] newRoute = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            Vector3 newCorner = LastSeenPosition + Random.insideUnitSphere * 10;
            newCorner.y = 1.3f;
            NavMeshHit hit;
            NavMesh.SamplePosition(newCorner, out hit, 2, 1);
            newCorner = hit.position;
            newRoute[i] = newCorner;
            Instantiate(DebugSphere, newCorner, Quaternion.identity);
        }
        return newRoute;
    }
}
