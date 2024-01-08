using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIBehaviourTree : MonoBehaviour
{
    [SerializeField] float searchingRadius;
    [SerializeField] float enemySearchingTime;
    [SerializeField] int searhingCornersNumber;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToLastEnemyPosition;
    [SerializeField] GameObject debugPoint;

    [Header("Patrolling")]
    [SerializeField] Route patrolRoute;
    [SerializeField] MovingMode patrolMovingMode;

    [Header("Behaviour Tree:")]
    [SerializeReference, SubclassSelector] Node root;

    public float SearchingRadius { get => searchingRadius; set => searchingRadius = value; }
    public float EnemySearchingTime { get => enemySearchingTime; set => enemySearchingTime = value; }
    public int SearhingCornersNumber { get => searhingCornersNumber; set => searhingCornersNumber = value; }
    public float DistanceToAttack { get => distanceToAttack; set => distanceToAttack = value; }
    public float DistanceToLastEnemyPosition { get => distanceToLastEnemyPosition; set => distanceToLastEnemyPosition = value; }
    public GameObject DebugPoint { get => debugPoint; set => debugPoint = value; }
    public Route PatrolRoute { get => patrolRoute; set => patrolRoute = value; }
    public MovingMode PatrolMovingMode { get => patrolMovingMode; set => patrolMovingMode = value; }
    public AINavigationManager AINavigationManager => navigationManager;
    public Sensors Sensors => sensors;  
    public Transform CurrentEnemy => sensors.CurrentEnemy;
    public ActionManager ActionManager => actionManager;
    public Vector3 LastEnemyPosition { get; set; }

    AINavigationManager navigationManager;
    ActionManager actionManager;
    Sensors sensors;

    void Start()
    {
        navigationManager = GetComponent<AINavigationManager>();
        sensors = GetComponent<Sensors>();
        actionManager = GetComponent<ActionManager>();
        root.Initialize(this, null);
    }

    void Update()
    {
        if (root != null)
            root.Evaluate();
    }
}
