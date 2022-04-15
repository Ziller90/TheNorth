using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public AutoAimController autoAim;
    public ButtonsManager buttonsManager;
    public Animator humanAnimator;
    public Thrower thrower;
    public GameObject weaponInHand;
    public Vector3 distanceAttackTarget;
    public float maxDistanceForAutoAim;

    public bool isMeleeAttack;
    public bool isBlock;
    public bool isDistantAttack;
    public float standartThrowDistance;

    
    public void ThrowWeapon()
    {
        thrower.Throw(distanceAttackTarget);
    }
    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(gameObject.transform, maxDistanceForAutoAim))
        {
            distanceAttackTarget = autoAim.GetBestAim(gameObject.transform, maxDistanceForAutoAim);
            characterContoller.LookAtPoint(distanceAttackTarget);
        }
        else
        {
            distanceAttackTarget = gameObject.transform.position + gameObject.transform.forward * standartThrowDistance;
            characterContoller.LookAtPoint(distanceAttackTarget);
        }
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
