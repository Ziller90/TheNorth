using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Not : Node
{
    public override NodeState Evaluate()
    {
        var decoratedNode = children[0];
        if (decoratedNode != null)
        {
            switch (decoratedNode.Evaluate())
            {
                case NodeState.SUCCESS:
                    return NodeState.FAILURE;
                case NodeState.FAILURE:
                    return NodeState.SUCCESS;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
