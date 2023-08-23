using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class HumanoidInventory : MonoBehaviour
{
    [SerializeField] AnimationEvents animationEvents;
    [SerializeField] Creature creature;
    [SerializeField] Transform dropPosition;

    [SerializeField] Transform rightHandKeeper;
    [SerializeField] Transform leftHandKeeper;

    [SerializeField] ItemStack rightHandItemStack;
    [SerializeField] ItemStack leftHandItemStack;
    [SerializeField] ItemStack[] quickAccessItemStacks = new ItemStack[3];

    [SerializeField] Container dropSackPrefab;

    Container inventoryContainer;
    Item rightHandItem;
    Item leftHandItem;

    public ItemStack RightHandItemStack => rightHandItemStack;
    public ItemStack LeftHandItemStack => leftHandItemStack;
    public ItemStack[] QuickAccessItemStacks => quickAccessItemStacks;
    public Container InventoryContainer => inventoryContainer;
    public int MoneyAmount => moneyAmount;

    public Action moneyAmountUpdated;

    int moneyAmount;
 
    public void AddMoney(int money)
    {
        moneyAmount += money;
        moneyAmountUpdated?.Invoke();
    }

    public void RemoveMoney(int money)
    {
        moneyAmount -= money;
        moneyAmountUpdated?.Invoke();
    }

    public void Start()
    {
        inventoryContainer = GetComponent<Container>();
    }

    public void AddItem(Item item)
    {
        var itemPrefab = Links.instance.globalLists.GetItemById(item.Id);
        ItemStack newItemStack;

        if (itemPrefab != null)
        {
            newItemStack = new ItemStack(itemPrefab, 1);
        }
        else
        {
            Debug.LogError("No item with such ID");
            return;
        }

        if (item.ItemUsingType == ItemUsingType.RightHand && rightHandItemStack == null)
        {
            RemoveItemFromWorld(item);
            SetItemStackInRightHand(newItemStack);
        }
        else if (item.ItemUsingType == ItemUsingType.LeftHand && leftHandItemStack == null)
        {
            RemoveItemFromWorld(item);
            SetItemStackInLeftHand(newItemStack);
        }
        else if (item.ItemUsingType == ItemUsingType.ActiveUsable)
        {
            bool addedToQuickAcessPanel = ModelUtils.AddItemStackToGroup(newItemStack, quickAccessItemStacks);
            if (addedToQuickAcessPanel)
                RemoveItemFromWorld(item);
            else
                AddItemToInventory(item, newItemStack);
        }
        else
        {
            AddItemToInventory(item, newItemStack);
        }
    }
    public  void AddItemToInventory(Item item, ItemStack itemStack)
    {
        bool added = ModelUtils.AddItemStackToGroup(itemStack, inventoryContainer.ItemsStacksInContainer);
        if (added)
            RemoveItemFromWorld(item);
        else
            Debug.LogError("Inventory is full");
    }
    
    public void RemoveItemFromWorld(Item item)
    {
        item.GetComponent<InteractableObject>().SetInteractable(false);
        Destroy(item.gameObject);
    }
    public void SetItemEquiped(Item item)
    {
        item.SetItemEquiped(true);
        item.GetComponent<InteractableObject>().SetInteractable(false);
    }
    public void DropItemsStack(ItemStack itemsStack)
    {
        if (rightHandItemStack == itemsStack)
            RemoveItemFromRightHand();
        else if (leftHandItemStack == itemsStack)
            RemoveItemFromLeftHand();
        else if (inventoryContainer.Contains(itemsStack))
            inventoryContainer.RemoveItemsStack(itemsStack);

        var dropSack = Instantiate(dropSackPrefab, dropPosition.position, Quaternion.identity);
        ModelUtils.AddItemStackToGroup(itemsStack, dropSack.ItemsStacksInContainer);
    }
    public void UseItem(ItemStack itemStack)
    {
        var itemUsing = itemStack.Item.GetComponent<ItemUsing>();
        if (itemUsing)
        {
            itemUsing.UseItem(creature);
            if (itemUsing.DestroyOnUse)
                itemStack.ItemsNumber -= 1;
        }
    }
    public void SetItemStackInRightHand(ItemStack itemsStack)
    {
        rightHandItemStack = itemsStack;
        rightHandItem = Instantiate(rightHandItemStack.Item, rightHandKeeper);
        rightHandItem.transform.rotation = rightHandKeeper.rotation;
        SetItemEquiped(rightHandItem);

        var meleeWeapon = rightHandItem.GetComponentInChildren<MeleeWeapon>();
        if (meleeWeapon)
        {
            meleeWeapon.SetWeaponHolder(creature);
            animationEvents.SetMeleeWeapon(meleeWeapon);
        }
    }
    public void SetItemStackInLeftHand(ItemStack itemsStack)
    {
        leftHandItemStack = itemsStack;
        leftHandItem = Instantiate(itemsStack.Item, leftHandKeeper);
        leftHandItem.transform.rotation = leftHandKeeper.rotation;
        SetItemEquiped(leftHandItem);
    }

    public void RemoveItemFromRightHand()
    {
        Destroy(rightHandItem.gameObject);
        rightHandItem = null;
        rightHandItemStack = new ItemStack(null, 0);
    }
    public void RemoveItemFromLeftHand()
    {
        Destroy(leftHandItem.gameObject);
        leftHandItem = null;
        leftHandItemStack = new ItemStack(null, 0);
    }

    public void SetItemInQuickAccessSlot(ItemStack itemsStack, int index)
    {
        quickAccessItemStacks[index] = itemsStack;
    }

    public void RemoveItemFromQuickAccessSlot(int index)
    {
        quickAccessItemStacks[index] = new ItemStack(null, 0);
    }
}
