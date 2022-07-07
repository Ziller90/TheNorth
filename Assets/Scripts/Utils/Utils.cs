using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetDirection(float Horizontal, float Vertical, float FixAngle)
    {
        Quaternion FixQuaternion = Quaternion.Euler(0, FixAngle, 0);
        Vector3 Direction = Vector3.ClampMagnitude(new Vector3(Horizontal, 0, Vertical), 1);
        Direction = FixQuaternion * Direction;
        return Direction;
    }
    public static float Round(float Number, int RoundIndex)
    {
        float temp = Number * RoundIndex;
        temp = (int)temp;
        temp = temp / RoundIndex;
        return (temp);
    }
    public static float SpeedConverter(float kmPerHour)
    {
        return (kmPerHour * 1000f / 3600f) / 50f;
    }
    public static Transform GetNearest(Transform start, List<Transform> points)
    {
        Transform nearestPoint = null;
        float bestDistance = 10000;
        foreach (Transform point in points)
        {
            float distance = Vector3.Distance(start.position, point.position);
            if (distance < bestDistance)
            {
                nearestPoint = point;
                bestDistance = distance;
            }
        }
        return nearestPoint;
    }
    public static Vector3 GetNearest(Vector3 start, List<Vector3> points)
    {
        Vector3 nearestPoint = Vector3.zero;
        float bestDistance = 10000;
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(start, point);
            if (distance < bestDistance)
            {
                nearestPoint = point;
                bestDistance = distance;
            }
        }
        return nearestPoint;
    }
}
