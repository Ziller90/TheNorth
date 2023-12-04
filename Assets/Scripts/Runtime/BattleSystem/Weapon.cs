using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines animation that should be used for this weapon
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
    ItemStack itemStackInInventory;

    public WeaponType Type => weaponType;
    public ItemStack ItemStack => itemStackInInventory;

    public void SetItemStack(ItemStack itemStack)
    {
        itemStackInInventory = itemStack;   
    }
}
