using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimController : MonoBehaviour
{
    public bool HasAutoAimTarget(GameObject thisUnit, Transform throwPoint, float maxDistance)
    {
        foreach (Transform aim in Game.ActorsAccessModel.Units)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance && thisUnit != aim.gameObject)
            {
                return true; 
            }
        }
        return false;
    }

    public Vector3 GetBestAim(GameObject thisUnit, Transform throwPoint, float maxDistance)
    {
        Vector3 bestAim = throwPoint.forward;
        float minAngle = 180f;
        foreach (Transform aim in Game.ActorsAccessModel.Units)
        {
            if (Vector3.Distance(throwPoint.position, aim.position) <= maxDistance && thisUnit != aim.gameObject)
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
