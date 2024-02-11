using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UseWeapon : Node
{
    enum Weapon
    {
        mainWeapon, secondaryWeapon
    }

    [SerializeField] Weapon weapon;
    public override NodeState Evaluate()
    {
        tree.NavigationManager.Stand();
        if (weapon == Weapon.mainWeapon)
        {
            tree.ActionManager.MainWeaponPressed();
            tree.ActionManager.MainWeaponReleased();
        }
        else if (weapon == Weapon.secondaryWeapon)
        {
            tree.ActionManager.SecondaryWeaponPressed();
            tree.ActionManager.SecondaryWeaponReleased();
        }

        state = NodeState.RUNNING;
        return state;
    }
}
