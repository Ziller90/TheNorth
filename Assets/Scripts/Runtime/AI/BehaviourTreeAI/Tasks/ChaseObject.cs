using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChaseObject : Node
{
    [SerializeField] GameObjectKey objectKey;
    [SerializeField] ComponentKey caughtRangeKey;
    [SerializeField] Vector3Key lastObjectPositionKey;

    public override NodeState Evaluate()
    {
        GameObject obj = tree.GetBlackboardValue(objectKey) as GameObject;
        Range caughtRange = tree.GetBlackboardValue(caughtRangeKey) as Range;

        if (obj != null)
        {
            tree.SetBlackBoardKeyValue(lastObjectPositionKey, obj.transform.position);
            if (!caughtRange.IsPointInRange(obj.transform.position))
            {
                tree.NavigationManager.MoveToTarget(obj.transform.position, MovingMode.Run);

                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
