using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Idle : ActionNode
{
    protected override State OnUpdate()
    {
        context.GameObject.GetComponent<AINavigationManager>().Stand();
        return State.Success;
    }
}
