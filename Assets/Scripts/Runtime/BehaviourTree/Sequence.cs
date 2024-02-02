using System;
using System.Collections.Generic;

[Serializable]
public class Sequence : Node
{
    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    return NodeState.FAILURE;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    return NodeState.RUNNING;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
