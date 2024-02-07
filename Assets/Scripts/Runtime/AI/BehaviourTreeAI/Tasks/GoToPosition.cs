using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GoToPosition : Node
{
    [SerializeField] Vector3Key positionKey;

    [SerializeField] MovingMode movingMode;
    [SerializeField] float minDistanceToPosition = 2f;

    Vector3 position;
    public override NodeState Evaluate()    
    {
        position = (Vector3)tree.GetBlackboardValue(positionKey);

        if (position != Vector3.zero)
        {
            if (Vector3.Distance(tree.gameObject.transform.position, position) > minDistanceToPosition)
            {
                tree.NavigationManager.MoveToTarget(position, movingMode);

                state = NodeState.RUNNING;
                return state;
            }
            else
            {
                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
