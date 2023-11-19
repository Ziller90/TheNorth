using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<Transform> routeCorners;
    public Vector3[] GetCorners()
    {
        Vector3[] corners = new Vector3[routeCorners.Count];
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = routeCorners[i].position;
        }
        return corners;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < routeCorners.Count - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(routeCorners[i].position, routeCorners[i + 1].position);
        }
        Gizmos.color = Color.black;
        Gizmos.DrawLine(routeCorners[routeCorners.Count - 1].position, routeCorners[0].position);
    }
}
