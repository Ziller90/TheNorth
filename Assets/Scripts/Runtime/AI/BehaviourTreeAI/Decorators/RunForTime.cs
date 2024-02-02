using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RunForTime : Node
{
    [SerializeField] float time;

    float timerStartTime;

    public override NodeState Evaluate()
    {
        bool timeIsOver = false;
        if (timerStartTime != 0)
        {
            if (Time.time - timerStartTime >= time)
            {
                timeIsOver = true;
                timerStartTime = 0;
            }
        }
        else
        {
            timerStartTime = Time.time;
        }

        var decoratedNode = children[0];
        if (decoratedNode != null)
        {
            switch (decoratedNode.Evaluate())
            {
                case NodeState.RUNNING:
                    if (timeIsOver)
                        return NodeState.SUCCESS;
                    else
                        return NodeState.RUNNING;
                case NodeState.SUCCESS:
                    return NodeState.SUCCESS;
                case NodeState.FAILURE:
                    return NodeState.FAILURE;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
