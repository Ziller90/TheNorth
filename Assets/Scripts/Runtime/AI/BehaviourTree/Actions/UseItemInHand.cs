using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class UseItemInHand : ActionNode
{
    [SerializeField] Weapon weapon;
    AINavigationManager navigationManager;
    ActionManager actionManager;

    enum Weapon
    {
        mainWeapon, secondaryWeapon
    }

    protected override void OnStart()
    {
        navigationManager = context.GameObject.GetComponent<AINavigationManager>();
        actionManager = context.GameObject.GetComponent<ActionManager>();
    }

    protected override State OnUpdate()
    {
        navigationManager.Stand();
        if (weapon == Weapon.mainWeapon)
        {
            actionManager.MainWeaponPressed();
            actionManager.MainWeaponReleased();
        }
        else if (weapon == Weapon.secondaryWeapon)
        {
            actionManager.SecondaryWeaponPressed();
            actionManager.SecondaryWeaponReleased();
        }

        state = State.Running;
        return state;
    }
}
