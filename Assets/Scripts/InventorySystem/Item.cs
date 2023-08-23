using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class Item : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] int maxStackSize;
    [SerializeField] ItemUsingType itemUsingType;
    [SerializeField] bool equiped;
    public int MaxStackSize => maxStackSize;
    public int Id => id;
    public string Name => itemName;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
    public ItemUsingType ItemUsingType => itemUsingType;

    Rigidbody rgbody;
    Collider[] itemColliders;

    void OnEnable()
    {
        rgbody = GetComponent<Rigidbody>();
        itemColliders = GetComponents<Collider>();
        SetItemEquiped(equiped);
    }
    public void SetItemEquiped(bool isEquiped)
    {
        equiped = isEquiped;
        rgbody.isKinematic = equiped;
        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isEquiped;
        }
    }
}
