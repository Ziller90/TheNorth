using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsoleLogTask : Node
{
    [SerializeField] string consoleLogMessage;

    public ConsoleLogTask() : base() { }
    public ConsoleLogTask(List<Node> children) : base(children) { }

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

