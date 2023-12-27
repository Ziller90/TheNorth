using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;

public class ConsoleLogTask : BehaviorTree.Node
{
    string consoleLogMessage;

    public ConsoleLogTask(string consoleLogMessage)
    {
        this.consoleLogMessage = consoleLogMessage;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(consoleLogMessage);
        state = NodeState.RUNNING;
        return state;   
    }
}
