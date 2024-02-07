using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IsEquals : Node
{
    [SerializeReference, SubclassSelector] BlackboardKey key1;
    [SerializeReference, SubclassSelector] BlackboardKey key2;

    public override NodeState Evaluate()
    {
        var blackBoardKeyValue1 = tree.GetBlackboardValue(key1);
        var blackBoardKeyValue2 = tree.GetBlackboardValue(key2);
        state = blackBoardKeyValue1.Equals(blackBoardKeyValue2) ? NodeState.SUCCESS : NodeState.FAILURE;
        return state;
    }
}
