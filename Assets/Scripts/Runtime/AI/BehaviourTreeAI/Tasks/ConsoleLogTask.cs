using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsoleLogTask : Node
{
    [SerializeField] string consoleLogMessage;

    public override NodeState Evaluate()
    {
        Debug.Log(consoleLogMessage);
        state = NodeState.RUNNING;
        return state;
    }
}

