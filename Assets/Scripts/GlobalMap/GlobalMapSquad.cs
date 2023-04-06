using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GlobalMapSquad : MonoBehaviour
{
    [SerializeField] float speed;
    NavMeshAgent navigationAgent;
    bool isMovingToPosition;

    void Start()
    {
        navigationAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
    }
    public void MoveToPosition(Vector3 newPosition)
    {
        navigationAgent.SetDestination(newPosition);
        isMovingToPosition = true;
    }
}
