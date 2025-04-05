using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] FightManager battleSystem;
    [SerializeField] HumanoidInventoryContainer inventory;
    [SerializeField] StepSounds stepSounds;
    [SerializeField] Bow bow;

    public void SetBow(Bow bow) => this.bow = bow;
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

    public void SetMainWeaponCuttingTrue()
    {
        if (inventory.MainWeaponItem && inventory.MainWeaponItem.MeleeWeapon)
            inventory.MainWeaponItem.MeleeWeapon.SetCuttingAnimation(true);
    }

    public void SetMainWeaponCuttingFalse()
    {
        if (inventory.MainWeaponItem && inventory.MainWeaponItem.MeleeWeapon)
            inventory.MainWeaponItem.MeleeWeapon.SetCuttingAnimation(false);
    }

    public void SetSecondaryWeaponCuttingTrue()
    {
        if (inventory.SecondaryWeaponItem && inventory.SecondaryWeaponItem.MeleeWeapon)
            inventory.SecondaryWeaponItem.MeleeWeapon.SetCuttingAnimation(true);
    }

    public void SetSecondaryWeaponCuttingFalse()
    {
        if (inventory.SecondaryWeaponItem && inventory.SecondaryWeaponItem.MeleeWeapon)
            inventory.SecondaryWeaponItem.MeleeWeapon.SetCuttingAnimation(false);
    }
}
