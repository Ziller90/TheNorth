using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public HumanoidBattleSystem battleSystem;
    public MeleeWeapon characterMeleeWeapon;
    public void SetCuttingAnimationTrue()
    {
        characterMeleeWeapon.SetCuttingAnimation(true);
    }
    public void SetCuttingAnimationFalse()
    {
        characterMeleeWeapon.SetCuttingAnimation(false);
    }
    public void AutoAim()
    {
        battleSystem.Aim();
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
