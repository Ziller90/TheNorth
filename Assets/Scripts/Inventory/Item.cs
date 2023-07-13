using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] bool inInventory;
    Rigidbody rgbody;
    Collider[] itemColliders;
    public ItemData ItemData => itemData;

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
