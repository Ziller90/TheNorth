using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    None = 0,
    MainWeapon = 1,
    SecondaryWeapon = 2,
    TwoHanded = 4,
    BothHanded = 8,
    Torso = 16,
    Helmet = 32,
    Trousers = 64,
    Boots = 128,
    QuikAcess = 256,
}

public enum EquipPositon
{
    None = 0,
    RightHand = 1,
    LeftHand = 2,
    BothHand = 3,
    Spine = 4
}

public class Item : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] int maxStackSize;
    [SerializeField] SlotType suitableSlots;
    [SerializeField] EquipPositon equipPositon;

    [SerializeField] bool equipedCached;
    [SerializeField] Collider[] itemColliders;
    public int MaxStackSize => maxStackSize;
    public int Id => id;
    public string Name => itemName;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
    public SlotType SuitableSlots => suitableSlots;
    public EquipPositon EquipPositon => equipPositon;

    Rigidbody rgbody;

    [ContextMenu("FindColliders")]
    public void FindItemColliders()
    {
        itemColliders = GetComponentsInChildren<Collider>();
    }

    void OnEnable()
    {
        rgbody = GetComponent<Rigidbody>();
        SetItemEquiped(equipedCached);
    }

    public void SetItemEquiped(bool isEquiped)
    {
        equipedCached = isEquiped;
        rgbody.isKinematic = equipedCached;
        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isEquiped;
        }
    }
}
