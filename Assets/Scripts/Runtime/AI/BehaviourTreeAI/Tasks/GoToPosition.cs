using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GoToPosition : Node
{
    [SerializeField] Vector3 position;
    [SerializeField] MovingMode movingMode;
    [SerializeField] float minDistanceToPosition = 2f;

    public override NodeState Evaluate()
    {
        if (position != Vector3.zero)
        {
            if (Vector3.Distance(tree.gameObject.transform.position, position) > minDistanceToPosition)
            {
                tree.AINavigationManager.MoveToTarget(tree.LastEnemyPosition, movingMode);

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
