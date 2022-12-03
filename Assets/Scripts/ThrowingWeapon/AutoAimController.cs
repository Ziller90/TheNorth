using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimController : MonoBehaviour
{
    List<Transform> aimObjects;
    void Start()
    {
        GlobalLists globalAims = Links.instance.globalLists;
        aimObjects = globalAims.creaturesOnLocation;
    }
    public bool HasAutoAimTarget(GameObject thisCreature, Transform throwPoint, float maxDistance)
    {
        foreach (Transform aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance && thisCreature != aim.gameObject)
            {
                return true; 
            }
        }
        return false;
    }
    public Vector3 GetBestAim(GameObject thisCreature, Transform throwPoint, float maxDistance)
    {
        Vector3 bestAim = throwPoint.forward;
        float minAngle = 180f;
        foreach (Transform aim in aimObjects)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance && thisCreature != aim.gameObject)
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
