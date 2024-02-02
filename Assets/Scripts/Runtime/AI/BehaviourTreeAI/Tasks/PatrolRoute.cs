using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PatrolRoute : Node
{
    float distanceToNextCorner = 1f;
    int nextCornerIndex;
    List<Vector3> currentRouteCorners;

    public override NodeState Evaluate()
    {
        currentRouteCorners = tree.PatrolRoute ? tree.PatrolRoute.Corners : null;

        if (currentRouteCorners == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        tree.AINavigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], tree.PatrolMovingMode);
        var unitPositionProjectionXZ = new Vector3(tree.AINavigationManager.transform.position.x, 0, tree.AINavigationManager.transform.position.z);
        var nextCornerPositionProjectionXZ = new Vector3(currentRouteCorners[nextCornerIndex].x, 0, currentRouteCorners[nextCornerIndex].z);
        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Count)
            {
                nextCornerIndex = 0;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
