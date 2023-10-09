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

    [SerializeField] ItemStack mainWeaponItemStack;
    [SerializeField] ItemStack secondaryWeaponItemStack;
    [SerializeField] ItemStack[] quickAccessItemStacks = new ItemStack[3];

    [SerializeField] Container dropSackPrefab;

    [SerializeField] HumanoidBattleSystem battleSystem;
    Container inventoryContainer;

    Item rightHandItem;
    Item leftHandItem;

    Item mainWeapon;
    Item secondaryWeapon;

    public ItemStack MainWeaponItemStack => mainWeaponItemStack;
    public ItemStack SecondaryWeaponItemStack => secondaryWeaponItemStack;
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

        if (item.SuitableSlots == SuitableSlotTypes.MainWeapon && mainWeaponItemStack.Item == null)
        {
            mainWeaponItemStack = newItemStack;

            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack);
        } 
        else if (item.SuitableSlots == SuitableSlotTypes.SecondaryWeapon && secondaryWeaponItemStack.Item == null)
        {
            secondaryWeaponItemStack = newItemStack;

            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack);
        }
        else if (item.SuitableSlots == SuitableSlotTypes.TwoHanded && secondaryWeaponItemStack.Item == null && mainWeaponItemStack.Item == null)
        {
            mainWeaponItemStack = newItemStack;
            secondaryWeaponItemStack = newItemStack;

            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack);
        }
        else if (item.SuitableSlots == SuitableSlotTypes.BothHanded)
        {
            if (mainWeaponItemStack.Item == null)
            {
                mainWeaponItemStack = newItemStack;
                RemoveItemFromWorld(item);
                SetItemStackInEquipmentPosition(newItemStack);
            }
            else if (secondaryWeaponItemStack.Item == null)
            {
                secondaryWeaponItemStack = newItemStack;
                RemoveItemFromWorld(item);
                SetItemStackInEquipmentPosition(newItemStack);
            }
        }
        else if (item.SuitableSlots == SuitableSlotTypes.QuikAcess && ModelUtils.AddItemStackToGroup(newItemStack, quickAccessItemStacks))
        {
            RemoveItemFromWorld(item);
        }
        else
        {
            AddItemToInventory(item, newItemStack);
        }
    }

    public void SetItemStackInEquipmentPosition(ItemStack itemStack)
    {
        Item instantiatedItem = null;
        if (itemStack.Item.EquipPositon == EquipPositon.RightHand)
        {
            instantiatedItem = InstantiateItemInHand(itemStack, rightHandKeeper);
            rightHandItem = instantiatedItem;
        }
        if (itemStack.Item.EquipPositon == EquipPositon.LeftHand)
        {
            instantiatedItem = InstantiateItemInHand(itemStack, leftHandKeeper);
            leftHandItem = instantiatedItem;
        }

        SetWeapon(instantiatedItem);
    }

    public void SetWeapon(Item item)
    {
        if (item && item.GetComponent<Weapon>() != null)
        {
            if (item.SuitableSlots == SuitableSlotTypes.MainWeapon)
            {
                mainWeapon = item;
                battleSystem.SetMainWeapon(item.GetComponent<Weapon>());
            }
            if (item.SuitableSlots == SuitableSlotTypes.SecondaryWeapon)
            {
                secondaryWeapon = item;
                battleSystem.SetSecondaryWeapon(item.GetComponent<Weapon>());
            }
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
        if (mainWeaponItemStack == itemsStack)
            RemoveMainWeapon();
        else if (secondaryWeaponItemStack == itemsStack)
            RemoveSecondaryWeapon();
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

    public Item InstantiateItemInHand(ItemStack itemsStack, Transform handKeeper)
    {
        var handItem = Instantiate(itemsStack.Item, handKeeper);
        handItem.transform.rotation = handKeeper.rotation;
        SetItemEquiped(handItem);

        var meleeWeapon = handItem.GetComponentInChildren<MeleeWeapon>();
        if (meleeWeapon)
        {
            meleeWeapon.SetWeaponHolder(creature);
            animationEvents.SetMeleeWeapon(meleeWeapon);
        }

        var bow = handItem.GetComponent<Bow>();
        if (bow)
        {
            animationEvents.SetBow(bow);
        }

        return handItem;
    }

    public void RemoveItemStackFromHand(ItemStack itemStack)
    {

    }
    public void RemoveMainWeapon()
    {
        if (mainWeapon == rightHandItem)
            RemoveEquipment(EquipPositon.RightHand);
        if (mainWeapon == leftHandItem)
            RemoveEquipment(EquipPositon.LeftHand);

        mainWeaponItemStack = new ItemStack(null, 0);
    }

    public void RemoveSecondaryWeapon()
    {
        if (secondaryWeapon == rightHandItem)
            RemoveEquipment(EquipPositon.RightHand);
        if (secondaryWeapon == leftHandItem)
            RemoveEquipment(EquipPositon.LeftHand);

        secondaryWeaponItemStack = new ItemStack(null, 0);
    }

    public void RemoveEquipment(EquipPositon equipPositon)
    {
        if (equipPositon == EquipPositon.RightHand)
        {
            Destroy(rightHandItem.gameObject);
            rightHandItem = null;
        }
        else if (equipPositon == EquipPositon.LeftHand)
        {
            Destroy(leftHandItem.gameObject);
            leftHandItem = null;
        }
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
