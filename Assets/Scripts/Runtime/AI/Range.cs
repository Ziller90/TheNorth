using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using SC = SiegeUp.Core;

public class Range : MonoBehaviour
{
    public enum RangeShapeType
    {
        Sphere,
        FOV,
        Box
    }

    [SerializeField] RangeShapeType rangeType;
    [SerializeField] float radius;
    [SerializeField] float angle;
    [SerializeField] Bounds bounds;

    public RangeShapeType RangeType => rangeType;
    public float Radius { get => radius; set => radius = value; }
    public float Angle { get => angle; set => angle = value; }
    public Bounds Bounds { get => bounds; set => bounds = value; }

    public bool IsPointInRange(Vector3 point)
    {
        switch (rangeType)
        {
            case RangeShapeType.Sphere:
                return SC.MathUtils.IsPointInRange(point, transform.position, radius);
            case RangeShapeType.Box:
                return bounds.Contains(transform.worldToLocalMatrix * (point - transform.position));
            case RangeShapeType.FOV:
                return IsPointInFOV(point, transform.position, transform.forward, radius, angle);
        }

        return false;
    }

    public static bool IsPointInFOV(Vector3 point, Vector3 origin, Vector3 forward, float FOVAngle, float maxDistance)
    {
        Vector3 directionToPoint = point - origin;

        if (directionToPoint.sqrMagnitude > maxDistance * maxDistance)
            return false;

        float angle = Vector3.Angle(forward, directionToPoint);

        return angle <= FOVAngle / 2;
    }
}
