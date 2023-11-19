using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] Route patrolRoute;
    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] MovingMode routeMovingMode;
    [SerializeField] Transform thisCreature;
    [SerializeField] float distanceToNextCorner;

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
        if (Vector3.Distance(thisCreature.transform.position, currentRouteCorners[nextCornerIndex]) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRouteCorners.Length)
            {
                nextCornerIndex = 0;
            }
        }
    }

}
