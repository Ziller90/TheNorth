using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public AutoAimController autoAim;
    public ButtonsManager buttonsManager;
    public Animator humanAnimator;
    public ThrowingWeapon thrower;
    public Vector3 autoAimTarget;

    public bool isMeleeAttack;
    public bool isBlock;
    public bool isDistantAttack;

    public void ThrowWeapon()
    {
        thrower.Throw(autoAimTarget);
    }
    public void AutoAim()
    {
        autoAimTarget = autoAim.GetBestAim(gameObject.transform);
        characterContoller.LookAtPoint(autoAimTarget);
    }
    public void DisableRunning()
    {
        characterContoller.allowRunning = false;
    }
    public void AllowRunning()
    {
        characterContoller.allowRunning = true;
    }
    public void StopAttack()
    {
        if (buttonsManager.isMeleeAttackButtonPressed == false)
        {
            isMeleeAttack = false;
        }
    }
    public void StopDistantAttack()
    {
        if (buttonsManager.isDistantAttackButtonPressed == false)
        {
            isDistantAttack = false;
        }
    }
    public void DisableMoving()
    {
        characterContoller.allowMoving = false;
    }
    public void AllowMoving()
    {
        if (buttonsManager.isMeleeAttackButtonPressed == false) 
        {
            characterContoller.allowMoving = true;
        }
    }
    public void DisableRotation()
    {
        characterContoller.allowRotation = false;
    }
    public void AllowRotation()
    {
        characterContoller.allowRotation = true;
    }
    public void Update()
    {
        if (buttonsManager.isMeleeAttackButtonPressed)
        {
            isMeleeAttack = true;
            DisableMoving();
        }
        humanAnimator.SetBool("Attack", isMeleeAttack);

        if (buttonsManager.isBlockButtonPressed)
        {
            isBlock = true;
            DisableRunning();
        }
        else
        {
            isBlock = false;
            AllowRunning();
        }
        humanAnimator.SetBool("ShieldUp", isBlock);

        if (buttonsManager.isDistantAttackButtonPressed)
        {
            isDistantAttack = true;
            DisableMoving();
            DisableRotation();
        }
        humanAnimator.SetBool("Throw", isDistantAttack);
    }
}
