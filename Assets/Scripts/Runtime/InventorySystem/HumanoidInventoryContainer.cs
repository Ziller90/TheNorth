using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanoidInventoryContainer : ContainerBase
{
    [SerializeField] Transform dropPosition;
    [SerializeField] Transform rightHandEquipPosition;
    [SerializeField] Transform leftHandEquipPosition;

    [SerializeField] Slot mainWeaponSlot;
    [SerializeField] Slot secondaryWeaponSlot;
    [SerializeField] SlotGroup quickSlots;
    [SerializeField] SlotGroup backpackSlots;

    [SerializeField] SimpleContainer dropSackPrefab;

    [SerializeField] GameObject unarmedHandItem;

    GameObject unarmedRightHand;
    GameObject unarmedLeftHand;

    Item rightHandItem;
    Item leftHandItem;

    Item mainWeaponItem;
    Item secondaryWeaponItem;

    public Slot MainWeaponSlot => mainWeaponSlot;
    public Slot SecondaryWeaponSlot => secondaryWeaponSlot;
    public SlotGroup QuickAccessSlots => quickSlots;
    public SlotGroup BackpackSlots => backpackSlots;
    public int MoneyAmount => moneyAmount;

    public Action moneyAmountUpdated;

    AnimationEvents animationEvents;
    HumanoidBattleSystem battleSystem;
    int moneyAmount;
    Unit unit;

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

    void Awake()
    {
        animationEvents = GetComponent<AnimationEvents>();
        battleSystem = GetComponent<HumanoidBattleSystem>();
        unit = GetComponent<Unit>();

        if (rightHandItem == null)
            unarmedRightHand = InstantiateUnarmedHandInEquipmentPosition(rightHandEquipPosition);
        if (leftHandItem == null)
            unarmedLeftHand = InstantiateUnarmedHandInEquipmentPosition(leftHandEquipPosition);

        InitAllSlots();
    }

    void OnEnable()
    {
        mainWeaponSlot.inserted += EquipMainWeapon;
        secondaryWeaponSlot.inserted += EquipSecondaryWeapon;

        mainWeaponSlot.removed += UnEquipMainWeapon;
        secondaryWeaponSlot.removed += UnEquipSecondaryWeapon;
    }

    void OnDisable()
    {
        mainWeaponSlot.inserted -= EquipMainWeapon;
        secondaryWeaponSlot.inserted -= EquipSecondaryWeapon;

        mainWeaponSlot.removed += UnEquipMainWeapon;
        secondaryWeaponSlot.removed += UnEquipSecondaryWeapon;
    }

    public bool TryPickUpItem(Item item)
    {
        var itemPrefab = Links.instance.globalLists.GetItemPrefabById(item.Id);
        if (itemPrefab == null)
        {
            Debug.LogError("No item with such ID");
            return false;
        }

        ItemStack newItemStack = new ItemStack(itemPrefab, 1);

        if (TryPickUpToMainWeaponSlot(newItemStack) ||
            secondaryWeaponSlot.TryAdd(newItemStack) ||
            quickSlots.TryAddOrMerge(newItemStack) ||
            backpackSlots.TryAddOrMerge(newItemStack))
        {
            DestroyItemOnScene(item);
            return true;
        }

        Debug.LogError("No free space in inventory");
        return false;
    }

    bool TryPickUpToMainWeaponSlot(ItemStack itemStack)
    {
        if (itemStack.Item.Tags.Contains(ItemTag.TwoHanded) && !secondaryWeaponSlot.isEmpty)
            return false;

        return mainWeaponSlot.TryAdd(itemStack);
    }

    void EquipMainWeapon()
    {
        var equipment = mainWeaponSlot.ItemStack.Item.GetComponent<Equipment>();
        Item instantiatedItem = null;

        if (equipment.EquipmentPosition == EquipmentPosition.BothHands ||
            equipment.EquipmentPosition == EquipmentPosition.RightHand ||
            equipment.EquipmentPosition == EquipmentPosition.TwoHands)
        {
            instantiatedItem = InstantiateItemInEquipmentPosition(mainWeaponSlot.ItemStack, rightHandEquipPosition);
            rightHandItem = instantiatedItem;
            Destroy(unarmedRightHand);
        }
        else if (equipment.EquipmentPosition == EquipmentPosition.LeftHand)
        {
            instantiatedItem = InstantiateItemInEquipmentPosition(mainWeaponSlot.ItemStack, leftHandEquipPosition);
            Destroy(unarmedLeftHand);
            leftHandItem = instantiatedItem;
        }

        if (mainWeaponSlot.ItemStack.Item.Tags.Contains(ItemTag.TwoHanded))
            EquipTwoHandedWeapon();

        SetMainWeapon(instantiatedItem);
    }

    void EquipTwoHandedWeapon()
    {
        if (!secondaryWeaponSlot.isEmpty)
        {
            ModelUtils.TryMoveFromSlotToSlotGroup(this, secondaryWeaponSlot, this, BackpackSlots);
        }
        secondaryWeaponSlot.SetBlock(true, mainWeaponSlot.ItemStack.Item.Icon);
    }

    void EquipSecondaryWeapon() 
    {
        var equipment = secondaryWeaponSlot.ItemStack.Item.GetComponent<Equipment>();
        Item instantiatedItem = null;

        if (equipment.EquipmentPosition == EquipmentPosition.BothHands ||
            equipment.EquipmentPosition == EquipmentPosition.LeftHand)
        {
            instantiatedItem = InstantiateItemInEquipmentPosition(secondaryWeaponSlot.ItemStack, leftHandEquipPosition);
            leftHandItem = instantiatedItem;
            Destroy(unarmedLeftHand);
        }

        SetSecondaryWeapon(instantiatedItem);   
    }

    void UnEquipMainWeapon(ItemStack removedItemStack)
    {
        if (mainWeaponItem == rightHandItem)
        {
            Destroy(rightHandItem.gameObject);
            unarmedRightHand = InstantiateUnarmedHandInEquipmentPosition(rightHandEquipPosition);
        }
        else if (mainWeaponItem == leftHandItem)
        {
            Destroy(leftHandItem.gameObject);
            unarmedLeftHand = InstantiateUnarmedHandInEquipmentPosition(leftHandEquipPosition);
        }

        if (removedItemStack.Item.Tags.Contains(ItemTag.TwoHanded))
            secondaryWeaponSlot.SetBlock(false, null);

        mainWeaponItem = null;
    }

    void UnEquipSecondaryWeapon(ItemStack removedItemStack)
    {
        if (secondaryWeaponItem == rightHandItem)
        {
            Destroy(rightHandItem.gameObject);
            unarmedRightHand = InstantiateUnarmedHandInEquipmentPosition(rightHandEquipPosition);
        }
        else if (secondaryWeaponItem == leftHandItem)
        {
            Destroy(leftHandItem.gameObject);
            unarmedLeftHand = InstantiateUnarmedHandInEquipmentPosition(leftHandEquipPosition);
        }

        secondaryWeaponItem = null;
    }

    void SetMainWeapon(Item item)
    {
        mainWeaponItem = item;
        battleSystem.SetMainWeapon(item.GetComponent<Weapon>());
    }

    void SetSecondaryWeapon(Item item)
    {
        secondaryWeaponItem = item;
        battleSystem.SetSecondaryWeapon(item.GetComponent<Weapon>());
    }
    
    void DestroyItemOnScene(Item item)
    {
        item.GetComponent<InteractableObject>().SetInteractable(false);
        Destroy(item.gameObject);
    }

    public void DropItemsStackFromSlot(Slot slot)
    {
        var dropSack = Instantiate(dropSackPrefab, dropPosition.position, Quaternion.identity);
        ModelUtils.TryMoveFromSlotToSlotGroup(this, slot, dropSack, dropSack.SlotGroup);
    }

    public void UseItemInSlot(Slot slot)
    {
        var itemStack = slot.ItemStack;

        bool success = ModelUtils.TryMoveFromSlotToSlot(this, slot, this, mainWeaponSlot) ||
                       ModelUtils.TryMoveFromSlotToSlot(this, slot, this, secondaryWeaponSlot);
        if (success)
            return;

        if (itemStack.Item.Tags.Contains(ItemTag.ActiveUsable))
            UseUsableItem(itemStack);
    }

    public void UseUsableItem(ItemStack itemStack)
    {
        var itemUsing = itemStack.Item.GetComponent<ItemUsing>();
        if (itemUsing)
        {
            itemUsing.UseItem(unit);
            if (itemUsing.DestroyOnUse)
                itemStack.ItemsNumber -= 1;
        }
    }

    GameObject InstantiateUnarmedHandInEquipmentPosition(Transform eqpuipmentPosition)
    {
        var item = Instantiate(unarmedHandItem, eqpuipmentPosition);
        item.transform.rotation = eqpuipmentPosition.rotation;
        var meleeWeapon = item.GetComponentInChildren<MeleeWeapon>();
        meleeWeapon.SetWeaponHolder(unit);
        animationEvents.SetMeleeWeapon(meleeWeapon);
        return item;
    }

    Item InstantiateItemInEquipmentPosition(ItemStack itemsStack, Transform eqpuipmentPosition)
    {
        var item = Instantiate(itemsStack.Item, eqpuipmentPosition);
        item.transform.rotation = eqpuipmentPosition.rotation;
        item.GetComponent<Equipment>().SetItemEquiped(true);

        var meleeWeapon = item.GetComponentInChildren<MeleeWeapon>();
        if (meleeWeapon)
        {
            meleeWeapon.SetWeaponHolder(unit);
            animationEvents.SetMeleeWeapon(meleeWeapon);
        }

        var bow = item.GetComponent<Bow>();
        if (bow)
        {
            animationEvents.SetBow(bow);
        }

        return item;
    }

    public override bool CanAddItemStackInSlot(ItemStack itemStack, Slot slot)
    {
        return CanEquipTwoHandedWeapon(itemStack, slot);
    }

    // can't equip TwoHanded weapon if two one handed weapons equiped and there is no place for them in inventory or if secondary weapon slot is not empty
    bool CanEquipTwoHandedWeapon(ItemStack itemStack, Slot slot) 
    {
        if (slot == mainWeaponSlot &&
            !mainWeaponSlot.isEmpty &&
            !secondaryWeaponSlot.isEmpty &&
            itemStack.Item.Tags.Contains(ItemTag.TwoHanded) &&
            backpackSlots.Slots.All(i => !i.isEmpty))
        {
            return false;
        }
        return true;
    }

    public void DivideItemStackInSlot(Slot slot)
    {
        var itemStackToDivide = slot.ItemStack;
        int newItemStackItemsNumber = Mathf.FloorToInt(itemStackToDivide.ItemsNumber / 2);
        itemStackToDivide.ItemsNumber -= newItemStackItemsNumber;
        var newItemStack = new ItemStack(itemStackToDivide.Item, newItemStackItemsNumber);

        if (BackpackSlots.isSlotInSlotGroup(slot) && BackpackSlots.CanAdd(newItemStack))
        {
            BackpackSlots.TryAddItemStackToSlotGroup(newItemStack);
        }
        else if (QuickAccessSlots.isSlotInSlotGroup(slot))
        {
            if (QuickAccessSlots.CanAdd(newItemStack))
            {
                QuickAccessSlots.TryAddItemStackToSlotGroup(newItemStack);
            }
            else if (BackpackSlots.CanAdd(newItemStack))
            {
                BackpackSlots.TryAddItemStackToSlotGroup(newItemStack);
            }
        }
    }

    public bool CanDivideItemStackInSlot(Slot slot)
    {
        if (slot.ItemStack.ItemsNumber > 1)
        {
            if (BackpackSlots.isSlotInSlotGroup(slot) && BackpackSlots.CanAdd(slot.ItemStack))
            {
                return true;
            }
            else if (QuickAccessSlots.isSlotInSlotGroup(slot))
            {
                if (QuickAccessSlots.CanAdd(slot.ItemStack))
                    return true;
                else if (BackpackSlots.CanAdd(slot.ItemStack))
                    return true;
            }
        }
        return false;
    }

    void InitAllSlots()
    {
        List<Slot> allContainerSlots = new List<Slot>(100);
        allContainerSlots.AddRange(backpackSlots.Slots);
        allContainerSlots.AddRange(quickSlots.Slots);
        allContainerSlots.Add(mainWeaponSlot);
        allContainerSlots.Add(secondaryWeaponSlot);

        foreach (var slot in allContainerSlots)
        {
            slot.SubscribeItemsNumberUpdateEvent();
        }
    }
}
