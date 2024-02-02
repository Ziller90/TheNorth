using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Idle : Node
{
    public override NodeState Evaluate()
    {
        tree.AINavigationManager.Stand();
        state = NodeState.RUNNING;
        return state;
    }
}