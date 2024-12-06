using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines animation that should be used for this weapon
public enum WeaponType
{
    None = 0,
    OneHanded = 10,
    TwoHanded = 20,
    Pickaxe = 21,
    Shield = 30,
    Bow = 40,
    ThrowingWeapon = 50
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
