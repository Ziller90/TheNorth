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
            if (Vector3.Distance(tree.transform.position, tree.CurrentEnemy.position) > tree.DistanceToAttack)
            {
                tree.AINavigationManager.MoveToTarget(tree.CurrentEnemy.position, MovingMode.Run);
                tree.ActionManager.mainWeaponUsing = false;

                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
