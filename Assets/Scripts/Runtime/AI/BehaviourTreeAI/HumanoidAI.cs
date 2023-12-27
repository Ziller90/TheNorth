using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class HumanoidAI : AIBehaviourTree
{
    [SerializeField] string consoleLogMessage;
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

    [SerializeField] HumanoidInventoryContainer AIInventory;

    Transform currentEnemy;

    protected override Node SetupTree()
    {
        Node root = new ConsoleLogTask(consoleLogMessage);
        return root;
    }
}
