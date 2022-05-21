using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Route patrolRoute;
    public Vector3[] patrolVectorRoute;
    public Vector3[] currentRoute;
    public int nextCornerIndex;
    public AINavigationManager navigationManager;
    public MovingMode routeMovingMode;
    public Transform ThisCreature;
    public float distanceToNextCorner;
    void Start()
    {
        patrolVectorRoute = GetVectorRoute(patrolRoute);
        currentRoute = patrolVectorRoute;
    }
    public void SetNewRoute(Vector3[] newRoute, int StartCorner)
    {
        currentRoute = newRoute;
        nextCornerIndex = StartCorner;
    }
    public void SetDefaultPatrolRoute()
    {
        currentRoute = patrolVectorRoute;
    }
    public void MoveOnRoute(MovingMode routeMovingMode)
    {
        navigationManager.MoveToTarget(currentRoute[nextCornerIndex], routeMovingMode);
        if (Vector3.Distance(ThisCreature.transform.position, currentRoute[nextCornerIndex]) < distanceToNextCorner)
        {
            nextCornerIndex++;
            if (nextCornerIndex == currentRoute.Length)
            {
                nextCornerIndex = 0;
            }
        }
    }
    void Update()
    {

    }
    public Vector3[] GetVectorRoute(Route route)
    {
        Vector3[] newVectorRoute = new Vector3[route.routeCorners.Count];
        for (int i = 0; i < newVectorRoute.Length; i++)
        {
            newVectorRoute[i] = route.routeCorners[i].position;
        }
        return newVectorRoute;
    }
}
