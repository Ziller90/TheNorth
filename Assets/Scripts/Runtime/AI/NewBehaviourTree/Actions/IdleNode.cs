using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IdleNode : ActionNode
{
    protected override void OnStart() { }
    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        context.GameObject.GetComponent<AINavigationManager>().Stand();
        return State.Success;
    }
}
