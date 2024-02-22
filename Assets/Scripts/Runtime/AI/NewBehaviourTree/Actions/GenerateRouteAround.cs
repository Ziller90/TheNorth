using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class GenerateRouteAround : ActionNode
{
    [Tooltip("Center around which route will be generated")]
    [SerializeField] NodeProperty<Vector3> routeCenter = new NodeProperty<Vector3>();
    [Tooltip("Key to save generated route")]
    [SerializeField] NodeProperty<Route> routeToSave = new NodeProperty<Route>();
    [SerializeField] float routeRadius;
    [SerializeField] int cornersNumber;

    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        if (routeCenter.Value != Vector3.zero)
        {
            var routeAroundPosition = InstantiateRouteAroundPosition("NewRoute", routeCenter.Value, cornersNumber, routeRadius);

            if (routeAroundPosition)
            {
                routeToSave.Value = routeAroundPosition;
                return State.Success;
            }
        }
        return State.Failure;
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
