using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Flags]
public enum ItemUsingType
{
    None = 0,
    RightHand = 1,
    LeftHand = 2,
    BothHand = 4,
    TwoHanded = 8,
    Torso = 16,
    Helmet = 32,
    Trousers = 64,
    Boots = 128,
    ActiveUsable = 256,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]

public class ItemData : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] ItemUsingType itemUsingType;
    [SerializeField] int maxStackSize;
    public string Name => itemName;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
    public ItemUsingType ItemUsingType => itemUsingType;
}
