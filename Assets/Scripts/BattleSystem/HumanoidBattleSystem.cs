using UnityEngine;

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
    bool isPlayingAttackAnimation;

    bool mainWeaponContinuousAttack;
    bool secondaryWeaponContinuousAttack;

    bool shieldRaised;
    int randomAttackIndex = 0;

    public bool ShieldRaised => shieldRaised;
    public float ShieldProtectionAngle => shieldProtectionAngle;

    public void SetShieldRaised(bool isRaised) => shieldRaised = isRaised;
    public void SetActionManager(ActionManager actionManager) => this.actionManager = actionManager;
    public void SetMainWeapon(Weapon weapon) => mainWeapon = weapon;
    public void SetSecondaryWeapon(Weapon weapon) => secondaryWeapon = weapon;
    public void DisableRunning() => characterContoller.AllowRunning = false;
    public void AllowRunning() => characterContoller.AllowRunning = true;
    public void DisableRotation() => characterContoller.AllowRotation = false;
    public void AllowRotation() => characterContoller.AllowRotation = true;
    public void DisableMoving() => characterContoller.AllowMoving = false;

    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(thisCreature, gameObject.transform, maxDistanceForAutoAim))
        {
            distantAttackTargetPosition = autoAim.GetBestAim(thisCreature, gameObject.transform, maxDistanceForAutoAim);
            characterContoller.LookAtPoint(distantAttackTargetPosition);
        }
    }

    public void AttackCompleted(int layer)
    {
        Debug.Log("AttackCompleted");
        isPlayingAttackAnimation = false;
        Debug.Log("isPlayingAttackAnimation = false");

        if (layer == 0)
        {
            if (characterContoller.ControlManager.MovingMode == MovingMode.Stand)
            {
                humanAnimator.CrossFadeInFixedTime("Idle", 0.20f, 0);
            }
            if (characterContoller.ControlManager.MovingMode == MovingMode.Walk)
            {
                humanAnimator.CrossFadeInFixedTime("Walk", 0.20f, 0);
            }
            if (characterContoller.ControlManager.MovingMode == MovingMode.Run)
            {
                humanAnimator.CrossFadeInFixedTime("Run", 0.20f, 0);
            }
        }

    }

    public void AttackStart(int layer)
    {
        isPlayingAttackAnimation = true;
    }

    public void AllowMoving()
    {
        characterContoller.AllowMoving = true;
    }

    void OnEnable()
    {
        actionManager.mainWeaponFastAttack += MainWeaponFastAttack;
        actionManager.mainWeaponPowerAttack += MainWeaponPowerAttack;
        actionManager.mainWeaponContinuousAttackStart += MainWeaponContinousAttackStart;
        actionManager.mainWeaponContinuousAttackStop += MainWeaponContinousAttackStop;

        actionManager.secondaryWeaponFastAttack += SecondaryWeaponFastAttack;
        actionManager.secondaryWeaponPowerAttack += SecondaryWeaponPowerAttack;
        actionManager.secondaryWeaponContinuousAttackStart += SecondaryWeaponContinousAttackStart;
        actionManager.secondaryWeaponContinuousAttackStop += SecondaryWeaponContinousAttackStop;
    }

    void OnDisable()
    {
        actionManager.mainWeaponFastAttack -= MainWeaponFastAttack;
        actionManager.mainWeaponPowerAttack -= MainWeaponPowerAttack;
        actionManager.mainWeaponContinuousAttackStart -= MainWeaponContinousAttackStart;
        actionManager.mainWeaponContinuousAttackStop -= MainWeaponContinousAttackStop;

        actionManager.secondaryWeaponFastAttack -= SecondaryWeaponFastAttack;
        actionManager.secondaryWeaponPowerAttack -= SecondaryWeaponPowerAttack;
        actionManager.secondaryWeaponContinuousAttackStart -= SecondaryWeaponContinousAttackStart;
        actionManager.secondaryWeaponContinuousAttackStart -= SecondaryWeaponContinousAttackStop;
    }

    public void MainWeaponFastAttack()
    {
        if (!isPlayingAttackAnimation)
        {
            if (!mainWeapon && !secondaryWeapon)
            {
                if (randomAttackIndex == 0)
                {
                    humanAnimator.CrossFadeInFixedTime("UnarmedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    humanAnimator.CrossFadeInFixedTime("UnarmedLeft", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon ||
                mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded ||
                mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                if (randomAttackIndex == 0)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedRight_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                if (randomAttackIndex == 0)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedLeft_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                humanAnimator.CrossFadeInFixedTime("UnarmedRight", 0.20f, 0);
                randomAttackIndex = 1;
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                var random = Random.Range(0, 3);
                if (random == 0)
                {
                    humanAnimator.CrossFadeInFixedTime("TwoHanded_1", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 1)
                {
                    humanAnimator.CrossFadeInFixedTime("TwoHanded_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 2)
                {
                    humanAnimator.CrossFadeInFixedTime("TwoHanded_3", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
        }
    }

    public void SecondaryWeaponFastAttack()
    {
        if (!isPlayingAttackAnimation)
        {
            if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                if (randomAttackIndex == 0)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    humanAnimator.CrossFadeInFixedTime("OneHandedLeft_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
        }
    }

    public void MainWeaponPowerAttack()
    {
        if (!isPlayingAttackAnimation)
        {
            if (!mainWeapon && !secondaryWeapon)
            {
                humanAnimator.CrossFadeInFixedTime("UnarmedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                humanAnimator.CrossFadeInFixedTime("OneHandedComboRight_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded && !mainWeapon)
            {
                humanAnimator.CrossFadeInFixedTime("OneHandedComboLeft_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                humanAnimator.CrossFadeInFixedTime("OneHandedComboRight_1", 0.20f, 0); // replace with both handed combo animation
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                humanAnimator.CrossFadeInFixedTime("TwoHandedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
        }
    }

    public void SecondaryWeaponPowerAttack()
    {
        if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
        {
            humanAnimator.CrossFadeInFixedTime("OneHandedComboLeft_1", 0.20f, 0); // replace with both handed combo animation
            isPlayingAttackAnimation = true;
        }
    }

    public void MainWeaponContinousAttackStart()
    {
        mainWeaponContinuousAttack = true;
    }

    public void SecondaryWeaponContinousAttackStart()
    {
        secondaryWeaponContinuousAttack = true;
    }

    public void MainWeaponContinousAttackStop()
    {
        mainWeaponContinuousAttack = false;
        humanAnimator.SetBool("MainWeaponContinuousAttacking", false);
    }

    public void SecondaryWeaponContinousAttackStop()
    {
        secondaryWeaponContinuousAttack = false;
        humanAnimator.SetBool("SecondaryWeaponContinuousAttacking", false);
    }

    public Vector3 GetHitVector(Vector3 hitPosition)
    {
        return transform.position - hitPosition;
    }

    private void Update()
    {
        if (mainWeaponContinuousAttack && !isPlayingAttackAnimation)
        {
            if (!mainWeapon && !secondaryWeapon)
            {

            }
        }

        if (secondaryWeaponContinuousAttack && !isPlayingAttackAnimation)
        {
            if (!mainWeapon && !secondaryWeapon)
            {
                humanAnimator.CrossFadeInFixedTime("UnarmedBlock", 0.20f, 1);
                humanAnimator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                humanAnimator.CrossFadeInFixedTime("OneHandedRightBlock", 0.20f, 1);
                humanAnimator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                humanAnimator.CrossFadeInFixedTime("ShieldBlock", 0.20f, 1);
                humanAnimator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
        }
    }

    void DebugLogAnimator()
    {
        var info = humanAnimator.GetAnimatorTransitionInfo(0);
        var infoState = humanAnimator.GetCurrentAnimatorStateInfo(0);
        var infoNextState = humanAnimator.GetNextAnimatorStateInfo(0);
        Debug.Log("TransitionInfo: " + "info.nameHash: " + info.nameHash + "info.normalizedTime: " + info.normalizedTime + "info.duration: " + info.duration + "info.durationUnit: " + info.durationUnit + "\n"
            + "StateInfo: " + "infoState.nameHash: " + infoState.fullPathHash + "infoState.normalizedTime: " + infoState.normalizedTime + "infoState.length: " + infoState.length + "infoState.shortNameHash: " + infoState.shortNameHash + "\n" +
            "NextStateInfo: " + "infoNextState.nameHash: " + infoNextState.fullPathHash + "infoNextState.normalizedTime: " + infoNextState.normalizedTime + "infoNextState.length: " + infoNextState.length + "infoNextState.shortNameHash: " + infoNextState.shortNameHash +
            "mainWeaponPowerAttacking: " + mainWeaponContinuousAttack + "secondaryWeaponPowerAttacking: " + secondaryWeaponContinuousAttack);
    }
}
