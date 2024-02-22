using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GetSensorsInformation : ActionNode
{
    [SerializeField] NodeProperty<GameObject> nearestEnemy = new NodeProperty<GameObject>();

    protected override State OnUpdate() 
    {
        nearestEnemy.Value = context.GameObject.GetComponent<Sensors>().NearestEnemy;
        return State.Success;
    }
}
