using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class PatrolRoute : ActionNode
{
    [SerializeField] NodeProperty<Route> route = new NodeProperty<Route>();
    [SerializeField] MovingMode movingMode;

    float distanceToNextCorner = 1f;
    int nextCornerIndex;
    List<Vector3> currentRouteCorners;
    AINavigationManager navigationManager;

    protected override void OnStart()
    {
        navigationManager = context.GameObject.GetComponent<AINavigationManager>();
    }

    protected override State OnUpdate()
    {
        currentRouteCorners = route.Value ? route.Value.Corners : null;

        if (currentRouteCorners == null)
        {
            state = State.Failure;
            return state;
        }

        var unitPositionProjectionXZ = new Vector3(navigationManager.transform.position.x, 0, navigationManager.transform.position.z);
        var nextCornerPositionProjectionXZ = new Vector3(currentRouteCorners[nextCornerIndex].x, 0, currentRouteCorners[nextCornerIndex].z);

        navigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], movingMode);

        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Count)
                nextCornerIndex = 0;
        }
        state = State.Running;
        return state;
    }
}
