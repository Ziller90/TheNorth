using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpdateTimer : Node
{
    [SerializeField] FloatKey timerKey;

    public override NodeState Evaluate()
    {
        var timer = (float)tree.GetBlackboardValue(timerKey);
        if (timer > 0)
            tree.SetBlackBoardKeyValue(timerKey, timer - Time.deltaTime);
        else
            tree.SetBlackBoardKeyValue(timerKey, 0f);

        state = NodeState.RUNNING;
        return state;
    }
}
