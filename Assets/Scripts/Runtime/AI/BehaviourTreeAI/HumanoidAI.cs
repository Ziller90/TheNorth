using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAI : MonoBehaviour
{
    [Header("AI Parts")]
    [SerializeField] Sensors sensors;
    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] ActionManager actionManager;
    [SerializeField] HumanoidInventoryContainer AIInventory;

    Transform currentEnemy;
    GameObject thisCreature;

    [SerializeField] float searchingRadius;
    [SerializeField] float enemySearchingTime;
    [SerializeField] int searhingCornersNumber;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToLastEnemyPosition;
    [SerializeField] RouteManager routeManager;
    [SerializeField] GameObject debugPoint;

    [SerializeField] AIBehaviourTree humanoidAITree;

    void Awake()
    {
        humanoidAITree.SetData("currentEnemy", currentEnemy);
        humanoidAITree.SetData("thisCreature", thisCreature);
    }
}
