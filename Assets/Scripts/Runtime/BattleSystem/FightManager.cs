using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] float shieldProtectionAngle;
    [SerializeField] float maxDistanceForAutoAim;

    CharacterContoller characterContoller;
    AutoAimController autoAim;
    ActionManager actionManager;
    Animator animator;

    Weapon mainWeapon;
    Weapon secondaryWeapon;

    Vector3 distantAttackTargetPosition;
    bool isPlayingAttackAnimation;
    bool shieldRaised;
    int randomAttackIndex = 0;

    bool mainWeaponContinuousAttack;
    bool secondaryWeaponContinuousAttack;

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

    void Awake()
    {
        characterContoller = GetComponent<CharacterContoller>();
        actionManager = GetComponent<ActionManager>();

        animator = GetComponent<Animator>();
    }

    public void Aim()
    {
        if (autoAim.HasAutoAimTarget(gameObject, gameObject.transform, maxDistanceForAutoAim))
        {
            distantAttackTargetPosition = autoAim.GetBestAim(gameObject, gameObject.transform, maxDistanceForAutoAim);
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
            if (characterContoller.CharacterMovingState == MovingState.Idle)
            {
                animator.CrossFadeInFixedTime("Idle", 0.20f, 0);
            }
            if (characterContoller.CharacterMovingState == MovingState.Walk)
            {
                animator.CrossFadeInFixedTime("Walk", 0.20f, 0);
            }
            if (characterContoller.CharacterMovingState == MovingState.Run)
            {
                animator.CrossFadeInFixedTime("Run", 0.20f, 0);
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
                    animator.CrossFadeInFixedTime("UnarmedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    animator.CrossFadeInFixedTime("UnarmedLeft", 0.20f, 0);
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
                    animator.CrossFadeInFixedTime("OneHandedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    animator.CrossFadeInFixedTime("OneHandedRight_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                if (randomAttackIndex == 0)
                {
                    animator.CrossFadeInFixedTime("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    animator.CrossFadeInFixedTime("OneHandedLeft_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                animator.CrossFadeInFixedTime("UnarmedRight", 0.20f, 0);
                randomAttackIndex = 1;
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                var random = Random.Range(0, 3);
                if (random == 0)
                {
                    animator.CrossFadeInFixedTime("TwoHanded_1", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 1)
                {
                    animator.CrossFadeInFixedTime("TwoHanded_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 2)
                {
                    animator.CrossFadeInFixedTime("TwoHanded_3", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.Pickaxe)
            {
                var random = Random.Range(0, 2);
                if (random == 0)
                {
                    animator.CrossFadeInFixedTime("Pickaxe_1", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 1)
                {
                    animator.CrossFadeInFixedTime("Pickaxe_2", 0.20f, 0);
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
                    animator.CrossFadeInFixedTime("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    animator.CrossFadeInFixedTime("OneHandedLeft_2", 0.20f, 0);
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
                animator.CrossFadeInFixedTime("UnarmedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                animator.CrossFadeInFixedTime("OneHandedComboRight_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded && !mainWeapon)
            {
                animator.CrossFadeInFixedTime("OneHandedComboLeft_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                animator.CrossFadeInFixedTime("OneHandedComboRight_1", 0.20f, 0); // replace with both handed combo animation
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                animator.CrossFadeInFixedTime("TwoHandedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
        }
    }

    public void SecondaryWeaponPowerAttack()
    {
        if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
        {
            animator.CrossFadeInFixedTime("OneHandedComboLeft_1", 0.20f, 0); // replace with both handed combo animation
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
        animator.SetBool("MainWeaponContinuousAttacking", false);
    }

    public void SecondaryWeaponContinousAttackStop()
    {
        secondaryWeaponContinuousAttack = false;
        animator.SetBool("SecondaryWeaponContinuousAttacking", false);
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
                animator.CrossFadeInFixedTime("UnarmedBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                animator.CrossFadeInFixedTime("OneHandedRightBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                animator.CrossFadeInFixedTime("ShieldBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
        }
    }
}
