using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackMelee : Node
{
    [SerializeField] ComponentKey currentEnemyTranformKey;
    [SerializeField] ComponentKey meleeAttackRangeKey;

    Range meleeAttackRange;
    Transform currentEnemy;

    public override NodeState Evaluate()
    {
        currentEnemy = tree.GetBlackboardValue(currentEnemyTranformKey) as Transform;
        meleeAttackRange = tree.GetBlackboardValue(meleeAttackRangeKey) as Range;

        if (currentEnemy != null)
        {
            if (meleeAttackRange.IsPointInRange(currentEnemy.position))
            {
                tree.NavigationManager.Stand();
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
