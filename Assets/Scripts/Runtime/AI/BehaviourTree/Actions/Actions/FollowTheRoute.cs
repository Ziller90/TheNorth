using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using SiegeUp;

[System.Serializable]
public class FollowTheRoute : ActionNode
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
        nextCornerIndex = 0;
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

        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Count)
            {
                state = State.Success;
                return state;
            }
        }
        navigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], movingMode);
        state = State.Running;
        return state;
    }

    protected override void OnStop()
    {
        Debug.Log("OnStop");
    }
}
