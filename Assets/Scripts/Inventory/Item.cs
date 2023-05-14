using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] bool inInventory;

    public ItemData ItemData => itemData;
    public bool InInventory { get => inInventory; set => inInventory = value; }

    void OnEnable()
    {
        if (!inInventory)
            Links.instance.globalLists.AddToItemsOnLocation(gameObject.transform);
    }
    void OnDisable()
    {
        if (!inInventory)
            Links.instance.globalLists.RemoveFromItemsOnLocation(gameObject.transform);
    }
}
