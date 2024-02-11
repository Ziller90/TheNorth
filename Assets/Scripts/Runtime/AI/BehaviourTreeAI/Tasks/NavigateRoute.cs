using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NavigateRoute : Node
{
    [SerializeField] ComponentKey patrolRouteKey;
    [SerializeField] MovingModeKey patrolMovingModeKey;
    [SerializeField] bool isLooped;

    Route patrolRoute;
    MovingMode patrolMovingMode;

    float distanceToNextCorner = 1f;
    int nextCornerIndex;
    List<Vector3> currentRouteCorners;

    public override NodeState Evaluate()
    {
        patrolRoute = tree.GetBlackboardValue(patrolRouteKey) as Route;
        patrolMovingMode = (MovingMode)tree.GetBlackboardValue(patrolMovingModeKey);

        currentRouteCorners = patrolRoute ? patrolRoute.Corners : null;

        if (currentRouteCorners == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        tree.NavigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], patrolMovingMode);
        var unitPositionProjectionXZ = new Vector3(tree.NavigationManager.transform.position.x, 0, tree.NavigationManager.transform.position.z);
        var nextCornerPositionProjectionXZ = new Vector3(currentRouteCorners[nextCornerIndex].x, 0, currentRouteCorners[nextCornerIndex].z);
        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Count)
            {
                nextCornerIndex = 0;
                if (!isLooped)
                {
                    state = NodeState.SUCCESS;
                    Debug.Log("FinishedRoute");
                    return state;
                }
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
