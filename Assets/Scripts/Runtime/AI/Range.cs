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
        СylinderSector,
        Box
    }

    [SerializeField] RangeShapeType rangeType;
    [SerializeField] float radius;
    [SerializeField] float height;
    [SerializeField] float sectorAngle;
    [SerializeField] Bounds bounds;

    public bool IsPointInRange(Vector3 point)
    {
        switch (rangeType)
        {
            case RangeShapeType.Sphere:
                return SC.MathUtils.IsPointInRange(point, transform.position, radius);
            case RangeShapeType.Box:
                return bounds.Contains(transform.worldToLocalMatrix * (point - transform.position));
            case RangeShapeType.СylinderSector:
                return SC.MathUtils.IsPointInCylinderSector(point, transform.position, radius, height, sectorAngle);
        }

        return false;
    }
}
