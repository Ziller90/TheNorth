using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public Animator humanAnimator;
    public bool isMeleeAttack;
    public bool isBlock;
    public bool isDistantAttack;


    public void Attack()
    {
        characterContoller.allowMoving = false;
        isMeleeAttack = true;
    }
    public void StopAttack()
    {
        characterContoller.allowMoving = true;
        isMeleeAttack = false;
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
        humanAnimator.SetBool("Attack", isMeleeAttack);
    }
}
