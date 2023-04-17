using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleSystem : MonoBehaviour
{
    [SerializeField] float shieldProtectionAngle;
    [SerializeField] CharacterContoller characterContoller;
    [SerializeField] AutoAimController autoAim;
    [SerializeField] ActionManager actionManager;
    [SerializeField] Animator humanAnimator;
    [SerializeField] Thrower thrower;
    [SerializeField] float maxDistanceForAutoAim;
    [SerializeField] GameObject throwingWeaponPrefab;
    [SerializeField] GameObject throwingWeaponInHand;
    [SerializeField] GameObject mainWeaponInHand;
    [SerializeField] GameObject thisCreature;
    [SerializeField] float standartThrowDistance;

    Vector3 distantAttackTargetPosition;
    bool isMeleeAttack;
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
        thrower.Throw(throwingWeaponPrefab, distantAttackTargetPosition);
    }
    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(thisCreature, gameObject.transform, maxDistanceForAutoAim))
        {
            distantAttackTargetPosition = autoAim.GetBestAim(thisCreature, gameObject.transform, maxDistanceForAutoAim);
            characterContoller.LookAtPoint(distantAttackTargetPosition);
        }
        else
        {
            distantAttackTargetPosition = gameObject.transform.position + gameObject.transform.forward * standartThrowDistance;
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
    public Vector3 GetHitVector(Vector3 hitPosition)
    {
        return transform.position - hitPosition;
    }
}
