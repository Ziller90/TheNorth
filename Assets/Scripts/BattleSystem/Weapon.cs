using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None = 0,
    OneHanded = 1,
    TwoHanded = 2,
    Shield = 3,
    Bow = 4,
    ThrowingWeapon = 5
}

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponType weaponType;
    public WeaponType Type => weaponType;
}
