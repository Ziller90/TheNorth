using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] bool inInventory;

    Collider[] itemColliders;
    public ItemData ItemData => itemData;

    void OnEnable()
    {
        itemColliders = GetComponents<Collider>();

        if (!inInventory)
            Links.instance.globalLists.AddToItemsOnLocation(gameObject.transform);
    }
    void OnDisable()
    {
        if (!inInventory)
            Links.instance.globalLists.RemoveFromItemsOnLocation(gameObject.transform);
    }
    public void SetItemState(bool isInInventory)
    {
        inInventory = isInInventory;
        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isInInventory;
        }
    }
}
