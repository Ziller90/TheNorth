using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] MeleeWeapon characterMeleeWeapon;
    [SerializeField] StepSounds stepSounds;
    [SerializeField] MeleeWeaponSounds meleeWeaponSounds;
    [SerializeField] Bow bow;

    public void SetMeleeWeapon(MeleeWeapon characterMeleeWeapon) => this.characterMeleeWeapon = characterMeleeWeapon;
    public void SetBow(Bow bow) => this.bow = bow;
    // public void SetThrowingWeaponInHand() => battleSystem.SetThrowingWeaponInHand();
    //public void MeleeWeaponAirCuttingSound() => meleeWeaponSounds.PlayAirCuttingSound();
    public void SetBlock() => battleSystem.SetShieldRaised(true);
    public void RemoveBlock() => battleSystem.SetShieldRaised(false);
    public void AutoAim() => battleSystem.Aim();
    public void AttackFinished(int layer) => battleSystem.AttackCompleted(layer);
    public void AttackStarted(int layer) => battleSystem.AttackStart(layer);
    public void AllowRotation() => battleSystem.AllowRotation();
    public void DisableRotation() => battleSystem.DisableRotation();
    public void AllowMoving() => battleSystem.AllowMoving();
    public void DisableMoving() => battleSystem.DisableMoving();
    public void AllowRuning() => battleSystem.AllowRunning();
    public void DisableRuning() => battleSystem.DisableRunning();
    public void PullBow() => bow.Pull();
    public void ReleseBow() => bow.Release();

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

    public void SetCuttingAnimationTrue()
    {
        if (characterMeleeWeapon)
            characterMeleeWeapon.SetCuttingAnimation(true);
    }
    public void SetCuttingAnimationFalse()
    {
        if (characterMeleeWeapon)
            characterMeleeWeapon.SetCuttingAnimation(false);
    }
}
