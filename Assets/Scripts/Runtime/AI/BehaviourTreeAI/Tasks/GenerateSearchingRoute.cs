using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class GenerateSearchingRoute : Node
{
    [SerializeField] ComponentKey routeAroundPositionKey;

    [SerializeField] Vector3Key routeCenterPositionKey;
    [SerializeField] int cornersNumber;
    [SerializeField] float radius;

    public override NodeState Evaluate()
    {
        Vector3 routeCenterPosition = (Vector3)tree.GetBlackboardValue(routeCenterPositionKey);
        var routeAroundPosition = InstantiateRouteAroundPosition("NewRoute", routeCenterPosition, cornersNumber, radius);

        if (routeAroundPosition)
        {
            tree.SetBlackBoardKeyValue(routeAroundPositionKey, routeAroundPosition);
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

    public Route InstantiateRouteAroundPosition(string routeName, Vector3 routeCenter, int cornersNumber, float radius)
    {
        var routeObj = new GameObject(routeName);
        var route = routeObj.AddComponent<Route>();
        route.Corners = GenerateRouteCornersAroundPosition(routeCenter, cornersNumber, radius);
        return route;
    }

    public List<Vector3> GenerateRouteCornersAroundPosition(Vector3 routeCenter, int cornersNumber, float radius)
    {
        List<Vector3> newRoute = new();
        for (int i = 0; i < cornersNumber; i++)
        {
            Vector3 newCorner = routeCenter + UnityEngine.Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(newCorner, out NavMeshHit hit, radius, 1))
            {
                newCorner = hit.position;
                newRoute.Add(newCorner);
            }
        }
        return newRoute;
    }
}
