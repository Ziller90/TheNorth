using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class HumanoidInventory : MonoBehaviour
{
    [SerializeField] AnimationEvents animationEvents;
    [SerializeField] Creature creature;
    [SerializeField] Transform itemsDropPosition;

    [SerializeField] Transform rightHandItemKeeper;
    [SerializeField] Transform leftHandItemKeeper;

    [SerializeField] Item rightHandItem;
    [SerializeField] Item leftHandItem;
    [SerializeField] Item[] quickAccessItems = new Item[3];

    Container inventoryContainer;

    public Item RightHandItem => rightHandItem;
    public Item LeftHandItem => leftHandItem;
    public Item[] QuickAccessItems => quickAccessItems;
    public Container InventoryContainer => inventoryContainer;

    public void Start()
    {
        inventoryContainer = GetComponent<Container>();
    }
    public void AddItem(Item item)
    {
        if (item.ItemData.ItemUsingType == ItemUsingType.RightHand && rightHandItem == null)
        {
            SetItemInRightHand(item);
            item.SetItemInInventory(true);
        }

        else if (item.ItemData.ItemUsingType == ItemUsingType.LeftHand && leftHandItem == null)
        {
            SetItemInLeftHand(item);
            item.SetItemInInventory(true);
        }
        else if (inventoryContainer.HasFreeSpace)
        {
            inventoryContainer.AddNewItem(item);
            item.transform.position = new Vector3(-1000, -1000, -1000);
            item.SetItemInInventory(true);
        }
        else
        {
            Debug.LogError("Inventory is full!");
        }
    }
    public void DropItem(Item item)
    {
        if (rightHandItem == item)
            RemoveItemFromRightHand();
        else if (leftHandItem == item)
            RemoveItemFromLeftHand();   
        else if (inventoryContainer.Contains(item))
            inventoryContainer.Remove(item);
        Links.instance.globalLists.AddInteractableOnLocation(item.GetComponent<InteractableObject>());
        item.SetItemInInventory(false);
        item.transform.position = itemsDropPosition.position;
    }
    public void UseItem(Item item)
    {
        var itemUsing = item.GetComponent<ItemUsing>();
        itemUsing.UseItem(creature);
    }
    public void SetItemInRightHand(Item item)
    {
        rightHandItem = item;
        item.transform.SetParent(rightHandItemKeeper);
        item.transform.position = rightHandItemKeeper.position;
        item.transform.rotation = rightHandItemKeeper.rotation;

        var meleeWeapon = item.GetComponentInChildren<MeleeWeapon>();
        if (meleeWeapon)
        {
            meleeWeapon.SetMeleeWeapon(creature);
            animationEvents.SetMeleeWeapon(meleeWeapon);
        }
    }
    public void SetItemInLeftHand(Item item)
    {
        leftHandItem = item;
        item.transform.SetParent(leftHandItemKeeper);
        item.transform.position = leftHandItemKeeper.position;
        item.transform.rotation = leftHandItemKeeper.rotation;

        var shield = item.GetComponentInChildren<MeleeWeapon>();
    }
    public void SetItemInQuickAccessSlot(Item item, int index)
    {
        quickAccessItems[index] = item;
    }
    public void RemoveItemFromRightHand()
    {
        rightHandItem.transform.position = new Vector3(-1000, -1000, -1000);
        rightHandItem = null;
    }
    public void RemoveItemFromLeftHand()
    {
        leftHandItem.transform.position = new Vector3(-1000, -1000, -1000);
        leftHandItem = null;
    }
    public void RemoveItemFromQuickAccessSlot(int index)
    {
        quickAccessItems[index] = null;
    }
}
