using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public bool isMeleeAttack;
    public float AttackTime;

    public void Attack()
    {
        characterContoller.isMeleeAttack = true;
    }
    public void StopAttack()
    {
        characterContoller.isMeleeAttack = false;
    }
}
