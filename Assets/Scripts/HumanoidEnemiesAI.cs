using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum States
{
    Chase,
    Patrol,
    Idle,
    Attack
}
public class HumanoidEnemiesAI : MonoBehaviour
{
    public Sensors sensors;
    public States AIState;
    public AINavigationManager navigationManager;
    public Transform enemy;

    void Start()
    {
        enemy = sensors.currentEnemy;
    }
    void Update()
    {
        enemy = sensors.currentEnemy;
        if (enemy != null)
            SetPositionToMove(enemy.position);
        switch (AIState)
        {
            case States.Attack:

                break;
            case States.Chase:

                break;
            case States.Patrol:

                break;
            case States.Idle:

                break;
        }
    }
    void SetPositionToMove(Vector3 target)
    {
        navigationManager.MoveToTarget(target);
    }
}
