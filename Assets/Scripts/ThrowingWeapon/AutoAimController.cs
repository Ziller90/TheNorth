using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimController : MonoBehaviour
{
    public List<Transform> aimObjects;
    void Start()
    {
        GlobalLists globalAims = LinksContainer.instance.globalLists;
        aimObjects = globalAims.aimsOnLocation;
    }
    public bool HasAutoAimTarget(Transform throwPoint, float maxDistance)
    {
        foreach (Transform aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance)
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
        foreach (Transform aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance) 
            {
                float angle = Vector3.Angle(throwPoint.forward, (aim.position - throwPoint.position));
                if (angle < minAngle)
                {
                    bestAim = aim.position;
                    minAngle = angle;
                }
            }
        }
        return bestAim;
    }
}
