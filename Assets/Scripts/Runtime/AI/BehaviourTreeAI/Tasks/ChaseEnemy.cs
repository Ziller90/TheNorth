using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChaseEnemy : Node
{
    Transform CurrentEnemy;
    public override NodeState Evaluate()
    {
        CurrentEnemy = tree.CurrentEnemy;
        if (tree.CurrentEnemy != null)
        {
            tree.LastEnemyPosition = tree.CurrentEnemy.position;
            if (!tree.MeleeAttackRange.IsPointInRange(CurrentEnemy.position))
            {
                tree.AINavigationManager.MoveToTarget(tree.CurrentEnemy.position, MovingMode.Run);

                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
