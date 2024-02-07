using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsoleLogTask : Node
{
    [SerializeField] StringKey consoleMessageKey;

    string consoleMessage;
    public override NodeState Evaluate()
    {
        consoleMessage = (string)tree.GetBlackboardValue(consoleMessageKey);

        Debug.Log(consoleMessage);
        state = NodeState.RUNNING;
        return state;
    }
}

