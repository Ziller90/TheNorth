using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] Route patrolRoute;
    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] MovingMode routeMovingMode;
    [SerializeField] Transform thisUnit;

    float distanceToNextCorner = 1f;
    int nextCornerIndex;
    Vector3[] patrolRouteCorners;
    Vector3[] currentRouteCorners;

    void Start()
    {
        if (patrolRoute)
        {
            patrolRouteCorners = patrolRoute.GetCorners();
            currentRouteCorners = patrolRouteCorners;
        }
    }

    public void SetNewRoute(Vector3[] newRoute, int StartCorner)
    {
        currentRouteCorners = newRoute;
        nextCornerIndex = StartCorner;
    }

    public void SetDefaultPatrolRoute()
    {
        currentRouteCorners = patrolRouteCorners;
    }

    public void MoveOnRoute(MovingMode routeMovingMode)
    {
        navigationManager.MoveToTarget(currentRouteCorners[nextCornerIndex], routeMovingMode);
        var unitPositionProjectionXZ = new Vector3(thisUnit.transform.position.x, 0, thisUnit.transform.position.z);
        var nextCornerPositionProjectionXZ = new Vector3(currentRouteCorners[nextCornerIndex].x, 0, currentRouteCorners[nextCornerIndex].z);
        if (Vector3.Distance(unitPositionProjectionXZ, nextCornerPositionProjectionXZ) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Length)
            {
                nextCornerIndex = 0;
            }
        }
    }

}
