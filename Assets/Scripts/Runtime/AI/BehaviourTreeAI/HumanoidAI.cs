using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAI : MonoBehaviour
{
    [Header("AI Parts")]
    [SerializeField] float searchingRadius;
    [SerializeField] float enemySearchingTime;
    [SerializeField] int searhingCornersNumber;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToLastEnemyPosition;
    [SerializeField] GameObject debugPoint;

    [SerializeField] AIBehaviourTree humanoidAITree;

    void Awake()
    {
        humanoidAITree.SetData("sensors", GetComponent<Sensors>());
        humanoidAITree.SetData("actionManager", GetComponent<ActionManager>());
        humanoidAITree.SetData("inventory", GetComponent<HumanoidInventoryContainer>());
        humanoidAITree.SetData("navigationManager", GetComponent<AINavigationManager>());
    }
}
