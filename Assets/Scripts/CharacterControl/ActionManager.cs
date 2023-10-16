 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ActionManager : MonoBehaviour
{
    [SerializeField] float clickTimeForFastAttack;
    [SerializeField] float powerAttackCooldown;

    bool mainWeaponWasPressed = false;
    float mainWeaponPressedTime;
    float timeOfLastPowerAttack;

    public void MainWeaponPressed()
    {
        mainWeaponPressedTime = Time.time;
        mainWeaponWasPressed = true;
    }

    public void MainWeaponReleased()
    {
        if (Time.time - mainWeaponPressedTime < clickTimeForFastAttack)
        {
            mainWeaponFastAttack?.Invoke();
        }
        mainWeaponWasPressed = false;
    }

    void Update()
    {
        if (mainWeaponWasPressed && Time.time - mainWeaponPressedTime > clickTimeForFastAttack && Time.time - timeOfLastPowerAttack > powerAttackCooldown)
        {
            mainWeaponPowerAttack?.Invoke();
            timeOfLastPowerAttack = Time.time;  
        }
    }

    public Action onOpenInventoryPressed;
    public Action onInteractPressed;

    public event Action mainWeaponFastAttack;
    public event Action mainWeaponPowerAttack;

    public event Action onSecondaryWeaponStartUsing;
    public event Action onSecondaryWeaponStopUsing;

    public bool mainWeaponUsing;
    public bool secondaryWeaponUsing;
}
