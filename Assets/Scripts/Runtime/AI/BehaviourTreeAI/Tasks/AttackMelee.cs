using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackMelee : Node
{
    Transform CurrentEnemy;

    public override NodeState Evaluate()
    {
        CurrentEnemy = tree.CurrentEnemy;
        if (tree.CurrentEnemy != null)
        {
            if (tree.MeleeAttackRange.IsPointInRange(CurrentEnemy.position))
            {
                tree.AINavigationManager.Stand();
                tree.ActionManager.MainWeaponPressed();
                tree.ActionManager.MainWeaponReleased();

                state = NodeState.RUNNING;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
