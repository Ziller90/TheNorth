using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GlobalMapSquad : MonoBehaviour
{
    [SerializeField] ControlManager controlManager;
    [SerializeField] float movingSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxDistanceToTarget;
    [SerializeField] float maxDistanceToLocation;
    [SerializeField] Animator SquadAnimator;

    Vector3[] pathCorners;
    Vector3 nextCorner;
    Vector3 target = Vector3.zero;

    [SerializeField] GameObject targetLocation;

    public void MoveToPosition(Vector3 target)
    {
        this.target = target;
        targetLocation = null; 
    }
    public void MoveToLocation(GameObject location)
    {
        MoveToPosition(location.transform.position);
        targetLocation = location;
    }
    void FixedUpdate()
    {
        if (target != Vector3.zero)
        {
            NavMeshPath newPath = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, newPath);
            pathCorners = newPath.corners;

            if (pathCorners.Length > 1 && Vector3.Distance(transform.position, pathCorners[pathCorners.Length - 1]) > maxDistanceToTarget)
            {
                nextCorner = pathCorners[1];
                Vector3 newDirection = (nextCorner - transform.position).normalized;
                newDirection = new Vector3(newDirection.x, 0, newDirection.z);
                Move(newDirection);
                
                if (targetLocation != null)
                {
                    var distanceToLocation = Vector3.Distance(transform.position, targetLocation.transform.position);
                    var locationRadius = targetLocation.GetComponent<CapsuleCollider>().radius;
                    if (distanceToLocation - locationRadius < maxDistanceToLocation)
                    {
                        Debug.Log("Get to location " + targetLocation.name);
                        targetLocation = null;
                        target = Vector3.zero;
                    }
                }
                return;
            }
        }
        SquadAnimator.SetInteger("MoveIndex", 1);
    }
    public void Move(Vector3 direction)
    {
        transform.position += transform.forward * movingSpeed;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
        SquadAnimator.SetInteger("MoveIndex", 3);
    }
    private void OnDrawGizmos()
    {
        if (pathCorners != null)
        {
            for (int i = 0; i < pathCorners.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pathCorners[i], 0.1f);
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
            }
        }
    }
}
