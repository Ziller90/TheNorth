using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChaseObjectNode : ActionNode
{
    [Tooltip("Object that AI should chase for")]
    [SerializeField] NodeProperty<GameObject> objectToChase = new NodeProperty<GameObject>();
    [SerializeField] MovingMode chasingMovingMode;

    [Tooltip("if object is in this range, node returns success")]
    [SerializeField]
    NodeProperty<Range> rangeToStop = new NodeProperty<Range>();

    [Tooltip("if object is in this radius, node returns success")]
    [SerializeField]
    NodeProperty<float> radiusToStop = new NodeProperty<float>();

    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        if (objectToChase.Value == null)
            return State.Failure;

        if (rangeToStop.Value != null && rangeToStop.Value.IsPointInRange(objectToChase.Value.transform.position))
            return State.Success;

        if (radiusToStop.Value != 0 && Vector3.Distance(context.Transform.position, objectToChase.Value.transform.position) < radiusToStop.Value)
            return State.Success;

        context.GameObject.GetComponent<AINavigationManager>().MoveToTarget(objectToChase.Value.transform.position, chasingMovingMode);
        return State.Running;
    }
}
