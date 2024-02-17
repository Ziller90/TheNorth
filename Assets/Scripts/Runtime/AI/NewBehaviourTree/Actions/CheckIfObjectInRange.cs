using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckIfObjectInRange : ActionNode
{
    [SerializeField] NodeProperty<GameObject> gameObjectToCheck = new NodeProperty<GameObject>();
    [SerializeField] NodeProperty<Range> range = new NodeProperty<Range>();

    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        if (gameObjectToCheck.Value != null)
        {
            if (range.Value.IsPointInRange(gameObjectToCheck.Value.transform.position))
                return State.Success;
        }
        return State.Failure;
    }
}
