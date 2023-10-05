using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        OneHanded,
        TwoHanded,
        Shield,
        Bow,
        ThrowingWeapon,
        ThrowingSpears,
        Bombs
    }

    WeaponType weaponType;
    public WeaponType Type => weaponType;
}
