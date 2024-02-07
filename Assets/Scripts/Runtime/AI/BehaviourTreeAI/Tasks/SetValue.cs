using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SetValue : Node
{
    [SerializeReference, SubclassSelector] BlackboardKey key1;
    [SerializeReference, SubclassSelector] BlackboardKey key2;

    public override NodeState Evaluate()
    {
        tree.SetBlackBoardKeyValue(key1, tree.GetBlackboardValue(key2));
        state = NodeState.SUCCESS;
        return state;
    }
}
