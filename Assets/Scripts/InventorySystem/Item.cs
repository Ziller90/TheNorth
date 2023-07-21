using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemInfo
{
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    public string Name => itemName;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
}
public class Item : MonoBehaviour
{
    [SerializeField] ItemInfo info;
    [SerializeField] ItemUsingType itemUsingType;
    [SerializeField] int maxStackSize;
    [SerializeField] bool inInventory;
    public ItemUsingType ItemUsingType => itemUsingType;
    public ItemInfo Info => info;

    Rigidbody rgbody;
    Collider[] itemColliders;

    void OnEnable()
    {
        rgbody = GetComponent<Rigidbody>();
        itemColliders = GetComponents<Collider>();
        SetItemInInventory(inInventory);
    }
    public void SetItemInInventory(bool isInInventory)
    {
        inInventory = isInInventory;
        rgbody.isKinematic = inInventory;
        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isInInventory;
        }
    }
}
