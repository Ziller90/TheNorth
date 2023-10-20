 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ActionManager : MonoBehaviour
{
    [SerializeField] float clickTimeForFastAttack;

    bool mainWeaponIsPressed = false;
    bool secondaryWeaponIsPressed = false;

    bool isMainWeaponPowerAttacking = false;
    bool isSecondaryWeaponPowerAttacking = false;

    float mainWeaponPressedTime;
    float secondaryWeaponPressedTime;

    public void MainWeaponPressed()
    {
        mainWeaponPressedTime = Time.time;
        mainWeaponIsPressed = true;
        mainWeaponContinuousAttackStart.Invoke();
    }

    public void MainWeaponReleased()
    {
        isMainWeaponPowerAttacking = false;
        
        if (Time.time - mainWeaponPressedTime < clickTimeForFastAttack)
        {
            mainWeaponFastAttack?.Invoke();
        }
        mainWeaponIsPressed = false;
        mainWeaponContinuousAttackStop.Invoke();
    }

    public void SecondaryWeaponPressed()
    {
        secondaryWeaponPressedTime = Time.time;
        secondaryWeaponIsPressed = true;
        secondaryWeaponContinuousAttackStart.Invoke();
    }

    public void SecondaryWeaponReleased()
    {
        isSecondaryWeaponPowerAttacking = false;
        
        if (Time.time - secondaryWeaponPressedTime < clickTimeForFastAttack)
        {
            secondaryWeaponFastAttack?.Invoke();
        }
        secondaryWeaponIsPressed = false;
        secondaryWeaponContinuousAttackStop.Invoke();
    }

    void Update()
    {
        if (mainWeaponIsPressed && Time.time - mainWeaponPressedTime > clickTimeForFastAttack && !isMainWeaponPowerAttacking)
        {
            mainWeaponPowerAttack?.Invoke();
            isMainWeaponPowerAttacking = true;
        }

        if (secondaryWeaponIsPressed && Time.time - secondaryWeaponPressedTime > clickTimeForFastAttack && !isSecondaryWeaponPowerAttacking)
        {
            secondaryWeaponPowerAttack?.Invoke();
            isSecondaryWeaponPowerAttacking = true;
        }
    }

    public Action onOpenInventoryPressed;
    public Action onInteractPressed;

    public event Action mainWeaponFastAttack;
    public event Action mainWeaponPowerAttack;

    public event Action mainWeaponContinuousAttackStart;
    public event Action mainWeaponContinuousAttackStop;

    public event Action secondaryWeaponFastAttack;
    public event Action secondaryWeaponPowerAttack;

    public event Action secondaryWeaponContinuousAttackStart;
    public event Action secondaryWeaponContinuousAttackStop;

    public bool mainWeaponUsing;
    public bool secondaryWeaponUsing;
}
