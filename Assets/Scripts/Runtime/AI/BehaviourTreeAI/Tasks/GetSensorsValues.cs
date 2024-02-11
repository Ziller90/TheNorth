using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GetSensorsValues : Node
{
    [SerializeField] GameObjectKey nearestEnemyKey;

    public override NodeState Evaluate()
    {
        tree.SetBlackBoardKeyValue(nearestEnemyKey,tree.Sensors.NearestEnemy);
        state = NodeState.RUNNING;
        return state;
    }
}
