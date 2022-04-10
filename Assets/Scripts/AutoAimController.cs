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
    public Vector3 GetBestAim(Transform throwPoint)
    {
        Vector3 bestAim = throwPoint.forward;
        float minAngle = 180f;
        foreach (Vector3 aim in aimObjects)
        {
            float angle = Vector3.Angle(throwPoint.forward, (aim - throwPoint.position));
            if (angle < minAngle)
            {
                bestAim = aim;
                minAngle = angle;
            }
        }
        return bestAim;
    }
}
