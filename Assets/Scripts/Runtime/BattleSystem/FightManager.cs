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
        if (autoAim.HasAutoAimTarget(gameObject, transform, maxDistanceForAutoAim))
        {
            distantAttackTargetPosition = autoAim.GetBestAim(gameObject, transform, maxDistanceForAutoAim);
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
                PlayAnimation("Idle", 0.20f, 0);
            }
            if (characterContoller.CharacterMovingState == MovingState.Walk)
            {
                PlayAnimation("Walk", 0.20f, 0);
            }
            if (characterContoller.CharacterMovingState == MovingState.Run)
            {
                PlayAnimation("Run", 0.20f, 0);
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
        actionManager.secondaryWeaponContinuousAttackStop -= SecondaryWeaponContinousAttackStop;
    }

    public void MainWeaponFastAttack()
    {
        if (!isPlayingAttackAnimation)
        {
            if (!mainWeapon && !secondaryWeapon)
            {
                if (randomAttackIndex == 0)
                {
                    PlayAnimation("UnarmedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    PlayAnimation("UnarmedLeft", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && (!secondaryWeapon ||
                     (secondaryWeapon && (secondaryWeapon.Type == WeaponType.OneHanded || secondaryWeapon.Type == WeaponType.Shield))))
            {
                if (randomAttackIndex == 0)
                {
                    PlayAnimation("OneHandedRight", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    PlayAnimation("OneHandedRight_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                if (randomAttackIndex == 0)
                {
                    PlayAnimation("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    PlayAnimation("OneHandedLeft_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (!mainWeapon && secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                PlayAnimation("UnarmedRight", 0.20f, 0);
                randomAttackIndex = 1;
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                int random = Random.Range(0, 3);
                if (random == 0)
                {
                    PlayAnimation("TwoHanded_1", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 1)
                {
                    PlayAnimation("TwoHanded_2", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 2)
                {
                    PlayAnimation("TwoHanded_3", 0.20f, 0);
                    randomAttackIndex = 0;
                    isPlayingAttackAnimation = true;
                }
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.Pickaxe)
            {
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    PlayAnimation("Pickaxe_1", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (random == 1)
                {
                    PlayAnimation("Pickaxe_2", 0.20f, 0);
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
            if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded &&
                secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                if (randomAttackIndex == 0)
                {
                    PlayAnimation("OneHandedLeft", 0.20f, 0);
                    randomAttackIndex = 1;
                    isPlayingAttackAnimation = true;
                }
                else if (randomAttackIndex == 1)
                {
                    PlayAnimation("OneHandedLeft_2", 0.20f, 0);
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
                PlayAnimation("UnarmedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                PlayAnimation("OneHandedComboRight_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded && !mainWeapon)
            {
                PlayAnimation("OneHandedComboLeft_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded &&
                     secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
            {
                PlayAnimation("OneHandedComboRight_1", 0.20f, 0); 
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.TwoHanded)
            {
                PlayAnimation("TwoHandedCombo_1", 0.20f, 0);
                isPlayingAttackAnimation = true;
            }
        }
    }

    public void SecondaryWeaponPowerAttack()
    {
        if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded &&
            secondaryWeapon && secondaryWeapon.Type == WeaponType.OneHanded)
        {
            PlayAnimation("OneHandedComboLeft_1", 0.20f, 0);
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
                PlayAnimation("UnarmedBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (mainWeapon && mainWeapon.Type == WeaponType.OneHanded && !secondaryWeapon)
            {
                PlayAnimation("OneHandedRightBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
            else if (secondaryWeapon && secondaryWeapon.Type == WeaponType.Shield)
            {
                PlayAnimation("ShieldBlock", 0.20f, 1);
                animator.SetBool("SecondaryWeaponContinuousAttacking", true);
                isPlayingAttackAnimation = true;
            }
        }
    }

    void PlayAnimation(string animationName, float transitionDuration, int layer)
    {
        animator.CrossFadeInFixedTime(animationName, transitionDuration, layer);
    }
}
