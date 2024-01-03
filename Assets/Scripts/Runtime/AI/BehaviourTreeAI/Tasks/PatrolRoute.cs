using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : Node
{
    [SerializeField] string consoleLogMessage;

    public PatrolRoute() : base() { }
    public PatrolRoute(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        Debug.Log(consoleLogMessage);
        state = NodeState.RUNNING;
        return state;
    }
}
