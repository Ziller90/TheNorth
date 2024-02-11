using System;
using UnityEngine;

[Serializable]
public class IsObjectInRange : Node
{
    [SerializeField] GameObjectKey objectToCheckKey;
    [SerializeField] ComponentKey rangeKey;

    Range range;
    GameObject objectToCheck;

    public override NodeState Evaluate()
    {
        objectToCheck = tree.GetBlackboardValue(objectToCheckKey) as GameObject;
        range = tree.GetBlackboardValue(rangeKey) as Range;

        if (objectToCheck != null && range.IsPointInRange(objectToCheck.transform.position))
        {
            state = NodeState.SUCCESS;
            return state;

        }
        state = NodeState.FAILURE;
        return state;
    }
}
