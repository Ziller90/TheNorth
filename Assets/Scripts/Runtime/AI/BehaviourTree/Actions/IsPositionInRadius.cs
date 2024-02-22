using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class IsPositionInRadius : ActionNode
{
    [SerializeField] NodeProperty<Vector3> positionToCheck = new NodeProperty<Vector3>();
    [SerializeField] NodeProperty<Vector3> radiusCenter = new NodeProperty<Vector3>();
    [SerializeField] NodeProperty<float> radius = new NodeProperty<float>();
    [SerializeField] bool useSelfPositionAsCenter;

    protected override State OnUpdate()
    {
        var center = useSelfPositionAsCenter ? context.Transform.position : radiusCenter.Value;

        if (Vector3.Distance(center, positionToCheck.Value) < radius.Value)
            return State.Success;
        return State.Failure;
    }
}
