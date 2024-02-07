using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChaseObject : Node
{
    [SerializeField] ComponentKey objectTransformKey;
    [SerializeField] ComponentKey caughtRangeKey;
    [SerializeField] Vector3Key lastObjectPositionKey;

    Transform objectTransform;
    Range caughtRange;
    public override NodeState Evaluate()
    {
        objectTransform = tree.GetBlackboardValue(objectTransformKey) as Transform;
        caughtRange = tree.GetBlackboardValue(caughtRangeKey) as Range;

        if (objectTransform != null)
        {
            tree.SetBlackBoardKeyValue(lastObjectPositionKey, objectTransform.position);
            if (!caughtRange.IsPointInRange(objectTransform.position))
            {
                tree.NavigationManager.MoveToTarget(objectTransform.position, MovingMode.Run);

                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
