using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    public CharacterContoller characterContoller;
    public AutoAimController autoAim;
    public ActionManager actionManager;
    public Animator humanAnimator;
    public Thrower thrower;
    public GameObject weaponInHand;
    public Vector3 distanceAttackTarget;
    public float maxDistanceForAutoAim;
    public GameObject throwingWeaponPrefab;
    public GameObject throwingWeaponInHand;
    public GameObject mainWeaponInHand;
    public GameObject thisCreature;

    public bool isMeleeAttack;
    public bool isBlock;
    public bool isDistantAttack;
    public float standartThrowDistance;

    public void SetThrowingWeaponInHand()
    {
        mainWeaponInHand.SetActive(false);
        throwingWeaponInHand.SetActive(true);
    }
    public void SetMainWeapon()
    {
        mainWeaponInHand.SetActive(true);
    }
    public void ThrowWeapon()
    {
        throwingWeaponInHand.SetActive(false);
        thrower.Throw(throwingWeaponPrefab, distanceAttackTarget);
    }
    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(thisCreature, gameObject.transform, maxDistanceForAutoAim))
        {
            distanceAttackTarget = autoAim.GetBestAim(thisCreature, gameObject.transform, maxDistanceForAutoAim);
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
        if (actionManager.isMeleeAttackActing == false)
        {
            isMeleeAttack = false;
        }
    }
    public void StopDistantAttack()
    {
        if (actionManager.isDistantAttackActing == false)
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
        if (actionManager.isMeleeAttackActing == false) 
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
        if (actionManager.isMeleeAttackActing)
        {
            isMeleeAttack = true;
            DisableMoving();
        }
        humanAnimator.SetBool("Attack", isMeleeAttack);

        if (actionManager.isBlockActing)
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

        if (actionManager.isDistantAttackActing)
        {
            isDistantAttack = true;
            DisableMoving();
            DisableRotation();
        }
        humanAnimator.SetBool("Throw", isDistantAttack);
    }
}
