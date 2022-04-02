using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public ButtonsManager buttonsManager;
    public Animator humanAnimator;
    public bool isMeleeAttack;
    public bool isBlock;
    public bool isDistantAttack;

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
            humanAnimator.SetBool("ShieldUp", true);
            DisableRunning();
        }
        else
        {
            isBlock = false;
            humanAnimator.SetBool("ShieldUp", false);
            AllowRunning();
        }


    }
}
