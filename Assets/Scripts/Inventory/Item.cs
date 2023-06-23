using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] bool inInventory;
    Rigidbody rgdody;
    Collider[] itemColliders;
    Interactable interactable;
    public ItemData ItemData => itemData;

    void OnEnable()
    {
        interactable = GetComponent<Interactable>();
        rgdody = GetComponent<Rigidbody>();
        itemColliders = GetComponents<Collider>();
        SetItemInInventory(inInventory);
    }
    public void SetItemInInventory(bool isInInventory)
    {
        interactable.IsInteractable = !isInInventory;
        inInventory = isInInventory;
        rgdody.isKinematic = inInventory;
        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isInInventory;
        }
    }
}
