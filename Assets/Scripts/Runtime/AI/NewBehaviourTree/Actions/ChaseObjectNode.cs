using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChaseObjectNode : ActionNode
{
    [Tooltip("Object that AI should chase for")]
    [SerializeField] NodeProperty<GameObject> objectToChase = new NodeProperty<GameObject>();
    [SerializeField] MovingMode chasingMovingMode;

    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        if (objectToChase.Value != null)
        {
            (context as UnitContext).NavigationManager.MoveToTarget(objectToChase.Value.transform.position, chasingMovingMode);
            return State.Success;
        }
        return State.Failure;
    }
}
