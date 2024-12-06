using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LookAtObject : ActionNode
{
    [SerializeField] NodeProperty<GameObject> objectToLookAt = new NodeProperty<GameObject>();

    protected override State OnUpdate()
    {
        context.Transform.LookAt(objectToLookAt.Value.transform);
        return State.Success;
    }
}
