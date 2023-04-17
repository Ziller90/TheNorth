using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] MeleeWeapon characterMeleeWeapon;
    [SerializeField] StepSounds stepSounds;
    [SerializeField] MeleeWeaponSounds meleeWeaponSounds;

    public void SetThrowingWeaponInHand()
    {
        battleSystem.SetThrowingWeaponInHand();
    }
    public void MeleeWeaponAirCuttingSound()
    {
        meleeWeaponSounds.PlayAirCuttingSound();
    }
    public void SetBlock()
    {
        battleSystem.SetShieldRaised(true);
    }
    public void RemoveBlock()
    {
        battleSystem.SetShieldRaised(false);
    }
    public void WalkingStep1()
    {
        stepSounds.SetStepSound1();
        stepSounds.isRunning = false;
        stepSounds.PlaySound();
    }
    public void WalkingStep2()
    {
        stepSounds.SetStepSound2();
        stepSounds.isRunning = false;
        stepSounds.PlaySound();
    }
    public void RunStep1()
    {
        stepSounds.SetStepSound1();
        stepSounds.isRunning = true;
        stepSounds.PlaySound();
    }
    public void RunStep2()
    {
        stepSounds.SetStepSound2();
        stepSounds.isRunning = true;
        stepSounds.PlaySound();
    }
    public void SetMainWeapon()
    {
        battleSystem.SetMainWeapon();
    }
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
