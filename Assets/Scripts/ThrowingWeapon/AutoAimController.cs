using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimController : MonoBehaviour
{
    public List<Vector3> aimObjects;
    void Start()
    {
        GlobalAims globalAims = LinksContainer.instance.globalAims;
        aimObjects = globalAims.globalAimsList;
    }
    public bool HasAutoAimTarget(Transform throwPoint, float maxDistance)
    {
        foreach (Vector3 aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim) <= maxDistance)
            {
                return true;
            }
        }
        return false;
    }
    public Vector3 GetBestAim(Transform throwPoint, float maxDistance)
    {
        Vector3 bestAim = throwPoint.forward;
        float minAngle = 180f;
        foreach (Vector3 aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim) <= maxDistance) { 
            float angle = Vector3.Angle(throwPoint.forward, (aim - throwPoint.position));
            if (angle < minAngle)
            {
                bestAim = aim;
                minAngle = angle;
            }
            }
        }
        return bestAim;
    }
}
