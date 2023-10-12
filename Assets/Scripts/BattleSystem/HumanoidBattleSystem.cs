using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System;

public class HumanoidBattleSystem : MonoBehaviour
{
    [SerializeField] float shieldProtectionAngle;
    [SerializeField] CharacterContoller characterContoller;
    [SerializeField] AutoAimController autoAim;
    [SerializeField] ActionManager actionManager;
    [SerializeField] Animator humanAnimator;
    [SerializeField] Thrower throwingWeaponThrower;
    [SerializeField] float maxDistanceForAutoAim;
    [SerializeField] GameObject thisCreature;

    [SerializeField] Weapon mainWeapon;
    [SerializeField] Weapon secondaryWeapon;

    Vector3 distantAttackTargetPosition;
    bool isMainWeaponAttack;
    bool isBlock;
    bool isDistantAttack;
    bool shieldRaised;

    public bool ShieldRaised => shieldRaised;
    public float ShieldProtectionAngle => shieldProtectionAngle;

    public void SetShieldRaised(bool isRaised)
    {
        shieldRaised = isRaised;
    }

    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }

    public void SetMainWeapon(Weapon weapon)
    {
        mainWeapon = weapon;
    }

    public void SetSecondaryWeapon(Weapon weapon)
    {
        secondaryWeapon = weapon;
    }

    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(thisCreature, gameObject.transform, maxDistanceForAutoAim))
        {
            distantAttackTargetPosition = autoAim.GetBestAim(thisCreature, gameObject.transform, maxDistanceForAutoAim);
            characterContoller.LookAtPoint(distantAttackTargetPosition);
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
        if (actionManager.mainWeaponUsing == false)
        {
            isMainWeaponAttack = false;
        }
    }

    public void DisableMoving()
    {
        characterContoller.allowMoving = false;
    }

    public void AllowMoving()
    {
        if (actionManager.mainWeaponUsing == false) 
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
        if (actionManager.mainWeaponUsing)
        {
            isMainWeaponAttack = true;
            DisableMoving();
        }


        if (!isMainWeaponAttack)
            humanAnimator.SetInteger("AttackIndex", 0);
        else
        {
            if (!mainWeapon)
                humanAnimator.SetInteger("AttackIndex", 1);
            else
                humanAnimator.SetInteger("AttackIndex", (int) mainWeapon.Type);
        }

        if (actionManager.secondaryWeaponUsing)
        {
            isBlock = true;
            DisableRunning();
        }
        else
        {
            isBlock = false;
            AllowRunning();
        }
        humanAnimator.SetBool("Shield", isBlock);
    }

    public Vector3 GetHitVector(Vector3 hitPosition)
    {
        return transform.position - hitPosition;
    }
}
