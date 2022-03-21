using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator humanAnimator;
    public CharacterContoller characterContoller;
    public MeleeAttack meleeAttack;


    void Start()
    {
        
    }

    void Update()
    {
        if (characterContoller.movingState == MovingState.Idle)
        {
            humanAnimator.SetInteger("MoveIndex", 1);
        }
        if (characterContoller.movingState == MovingState.Walk)
        {
            humanAnimator.SetInteger("MoveIndex", 2);
        }
        if (characterContoller.movingState == MovingState.Run)
        { 
            humanAnimator.SetInteger("MoveIndex", 3);
        }

        humanAnimator.SetBool("Attack", meleeAttack.isAttack);
    }
}
