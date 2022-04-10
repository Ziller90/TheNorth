using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public HumanoidBattleSystem battleSystem;
    public void AutoAim()
    {
        battleSystem.AutoAim();
    }
    public void Throwed()
    {
        battleSystem.ThrowWeapon();
    }
    public void AttackFinished()
    {
        battleSystem.StopAttack();
    }
    public void DistantAttackFinished()
    {
        battleSystem.StopDistantAttack();
    }
    public void DisableRotation()
    {
        battleSystem.DisableRotation();
    }
    public void AllowRotation()
    {
        battleSystem.AllowRotation();
    }
    public void AllowMoving()
    {
        battleSystem.AllowMoving();
    }
    public void DisableMoving()
    {
        battleSystem.DisableMoving();
    }
}
