using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PatrolRoute : Node
{
    [SerializeField] Route patrolRoute;
    [SerializeField] MovingMode routeMovingMode;

    AINavigationManager navigationManager;
    float distanceToNextCorner = 1f;
    int nextCornerIndex;
    Vector3[] patrolRouteCorners;
    Vector3[] currentRouteCorners;

    public void SetPatrolRoute(Route newRoute)
    {
        patrolRoute = newRoute; 
    }

    public override void OnInitialize()
    {
        navigationManager = (AINavigationManager)hostTree.GetData("navigationManager");

        if (patrolRoute)
        {
            patrolRouteCorners = patrolRoute.GetCorners();
            currentRouteCorners = patrolRouteCorners;
        }
    }

    public override NodeState Evaluate()
    {
        navigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], routeMovingMode);
        var unitPositionProjectionXZ = new Vector3(navigationManager.transform.position.x, 0, navigationManager.transform.position.z);
        var nextCornerPositionProjectionXZ = new Vector3(currentRouteCorners[nextCornerIndex].x, 0, currentRouteCorners[nextCornerIndex].z);
        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Length)
            {
                nextCornerIndex = 0;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
