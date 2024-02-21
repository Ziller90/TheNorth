using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GoToPositionNode : ActionNode
{
    [SerializeField] NodeProperty<Vector3> positionToGo = new NodeProperty<Vector3>();
    [SerializeField] MovingMode movingMode;

    [Tooltip("if position is in this range, node returns success")] [SerializeField]
    NodeProperty<Range> rangeToStop = new NodeProperty<Range>();

    [Tooltip("if position is in this radius, node returns success")] [SerializeField]
    NodeProperty<float> radiusToStop = new NodeProperty<float>();

    [SerializeField] bool MoveIfPositionIsZero = false;

    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        if (!MoveIfPositionIsZero && positionToGo.Value == Vector3.zero)
            return State.Failure;

        if (rangeToStop.Value != null && rangeToStop.Value.IsPointInRange(positionToGo.Value))
            return State.Success;

        if (radiusToStop.Value != 0 && Vector3.Distance(context.Transform.position, positionToGo.Value) < radiusToStop.Value)
            return State.Success;

        context.GameObject.GetComponent<AINavigationManager>().MoveToTarget(positionToGo.Value, movingMode); 
        return State.Running;
    }
}
