using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTag
{
    Weapon,
    OneHanded,
    TwoHanded,
    HeadArmor,
    BodyArmor,
    LegsArmor,
    FootwearArmor,
    Arrows,
    ThrowingWeapon,
    Shield,
    ActiveUsable,
    Usable,
    Food,
    Medicine,
    Sword,
    Axe,
    Dagger,
    Knife,
    Club,
    Trash,
    Ring,
    Amulet,
    Mace,
    Bow,
    Staff,
    Poison,
    Book,
    MiningTool
}

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] int maxStackSize;
    [SerializeField] List<ItemTag> itemTags;

    public int MaxStackSize => maxStackSize;
    public string Name => itemName;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
    public List<ItemTag> Tags  => itemTags;

    public MeleeWeapon MeleeWeapon => GetComponentInChildren<MeleeWeapon>();
}
