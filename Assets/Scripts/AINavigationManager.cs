using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AINavigationManager : MonoBehaviour
{
    public Vector3[] pathCorners;
    public Vector3 target;
    public bool moveToTarget;
    public NavMeshAgent navigationAgent;
    public ControlManager controlManager;
    public Vector3 nextCorner;
    void Start()
    {
        
    }

    public void MoveToTarget(Vector3 target)
    {
        this.target = target;
        moveToTarget = true;
    }

    void Update()
    {
        if (moveToTarget)
        {
            navigationAgent.destination = target;
            pathCorners = navigationAgent.path.corners;
            if (pathCorners.Length > 1)
            {
                nextCorner = pathCorners[1];
                controlManager.SetControl((nextCorner - navigationAgent.transform.position).normalized, 1f);
            }
            else
            {
                controlManager.SetControl(Vector3.zero, 0f);
            }
        }
    }
}
