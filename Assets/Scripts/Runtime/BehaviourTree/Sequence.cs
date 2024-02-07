using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sequence : Node
{
    [SerializeField] bool stopEvaluatingChildrenIfOneIsRunning;
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
                    if (stopEvaluatingChildrenIfOneIsRunning)
                        return NodeState.RUNNING;
                    else
                        continue;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
