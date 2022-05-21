using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AINavigationManager : MonoBehaviour
{
    public Vector3[] pathCorners;
    public Vector3 target;
    public ControlManager controlManager;
    public Vector3 nextCorner;
    public Transform thisCreature;
    public MovingMode movingMode;

    public void MoveToTarget(Vector3 target, MovingMode movingMode)
    {
        this.target = target;
        this.movingMode = movingMode;
    }
    public void Stand()
    {
        target = Vector3.zero;
        movingMode = MovingMode.Stand;
    }
    void Update()
    {
        if (movingMode != MovingMode.Stand)
        {
            NavMeshPath newPath = new NavMeshPath();
            NavMesh.CalculatePath(thisCreature.position, target, NavMesh.AllAreas, newPath);
            pathCorners = newPath.corners;
            if (pathCorners.Length > 1)
            {
                nextCorner = pathCorners[1];
                Vector3 newDirection = (nextCorner - thisCreature.transform.position).normalized;
                newDirection = new Vector3(newDirection.x, 0, newDirection.z);
                controlManager.SetControl(newDirection, movingMode);
            }
            else
            {
                controlManager.SetControl(Vector3.zero, MovingMode.Stand);
            }
        }
        else
        {
            controlManager.SetControl(Vector3.zero, MovingMode.Stand);
        } 
    }
}
