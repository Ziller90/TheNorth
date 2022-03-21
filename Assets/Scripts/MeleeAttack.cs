using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public bool isAttack;

    public void Attack()
    {
        characterContoller.allowMoving = false;
        isAttack = true;
    }
    public void StopAttack()
    {
        characterContoller.allowMoving = true;
        isAttack = false;
    }
    public void DisableRotation()
    {
        characterContoller.allowRotation = false;
    }
    public void AllowRotation()
    {
        characterContoller.allowRotation = true;
    }

}
