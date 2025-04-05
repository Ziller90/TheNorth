using SiegeUp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable, ComponentId(2)]
public class HumanoidInventoryContainer : ContainerBase
{
    [SerializeField] Transform dropPosition;
    [SerializeField] Transform rightHandEquipPosition;
    [SerializeField] Transform leftHandEquipPosition;

    [SerializeField] SimpleContainer dropSackPrefab;
    [SerializeField] Item unarmedHandItem;

    [AutoSerialize(1), SerializeField] Slot mainWeaponSlot;
    [AutoSerialize(2), SerializeField] Slot secondaryWeaponSlot;
    [AutoSerialize(3), SerializeField] SlotGroup quickSlots;
    [AutoSerialize(4), SerializeField] SlotGroup backpackSlots;

    Item rightHandItem;
    Item leftHandItem;

    Item mainWeaponItem;
    Item secondaryWeaponItem;

    public Item MainWeaponItem => mainWeaponItem;
    public Item SecondaryWeaponItem => secondaryWeaponItem;

    public Slot MainWeaponSlot => mainWeaponSlot;
    public Slot SecondaryWeaponSlot => secondaryWeaponSlot;
    public SlotGroup QuickAccessSlots => quickSlots;
    public SlotGroup BackpackSlots => backpackSlots;
    public int MoneyAmount => moneyAmount;

    public Action moneyAmountUpdated;

    AnimationEvents animationEvents;
    FightManager battleSystem;
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
        battleSystem = GetComponent<FightManager>();
        unit = GetComponent<Unit>();

        if (rightHandItem == null)
        {
            rightHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, rightHandEquipPosition);
            mainWeaponItem = rightHandItem;
        }
        if (leftHandItem == null)
        {
            leftHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, leftHandEquipPosition);
            secondaryWeaponItem = leftHandItem;
        }

        InitAllSlots();
    }

    private void Start()
    {
        UnSubsribeWeaponSlotsEvents();
        SubsribeWeaponSlotsEvents();

        if (!mainWeaponSlot.IsEmpty)
            EquipMainWeapon();

        if (!secondaryWeaponSlot.IsEmpty)
            EquipSecondaryWeapon();
    }

    void OnEnable()
    {
        UnSubsribeWeaponSlotsEvents();
        SubsribeWeaponSlotsEvents();
    }

    void OnDisable()
    {
        UnSubsribeWeaponSlotsEvents();
    }

    void SubsribeWeaponSlotsEvents()
    {
        mainWeaponSlot.inserted += EquipMainWeapon;
        secondaryWeaponSlot.inserted += EquipSecondaryWeapon;

        mainWeaponSlot.removed += UnEquipMainWeapon;
        secondaryWeaponSlot.removed += UnEquipSecondaryWeapon;
    }

    void UnSubsribeWeaponSlotsEvents()
    {
        mainWeaponSlot.inserted -= EquipMainWeapon;
        secondaryWeaponSlot.inserted -= EquipSecondaryWeapon;

        mainWeaponSlot.removed -= UnEquipMainWeapon;
        secondaryWeaponSlot.removed -= UnEquipSecondaryWeapon;
    }

    public bool TryPickUpItem(Item item)
    {
        var itemPrefab = Game.PrefabManager.GetPrefabRef(item.GetComponent<PrefabRef>().GetGuid());
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
        if (itemStack.Item.Tags.Contains(ItemTag.TwoHanded) && !secondaryWeaponSlot.IsEmpty)
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
            if (rightHandItem)
                Destroy(rightHandItem);

            instantiatedItem = InstantiateItemInEquipmentPosition(mainWeaponSlot.ItemStack.Item, rightHandEquipPosition);
            rightHandItem = instantiatedItem;
        }
        else if (equipment.EquipmentPosition == EquipmentPosition.LeftHand)
        {
            if (leftHandItem)
                Destroy(leftHandItem);

            instantiatedItem = InstantiateItemInEquipmentPosition(mainWeaponSlot.ItemStack.Item, leftHandEquipPosition);
            leftHandItem = instantiatedItem;
        }

        if (mainWeaponSlot.ItemStack.Item.Tags.Contains(ItemTag.TwoHanded))
            EquipTwoHandedWeapon();

        SetMainWeapon(instantiatedItem);
    }

    void EquipTwoHandedWeapon()
    {
        if (!secondaryWeaponSlot.IsEmpty)
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
            if (leftHandItem)
                Destroy(leftHandItem);

            instantiatedItem = InstantiateItemInEquipmentPosition(secondaryWeaponSlot.ItemStack.Item, leftHandEquipPosition);
            leftHandItem = instantiatedItem;
        }

        SetSecondaryWeapon(instantiatedItem);   
    }

    void UnEquipMainWeapon(ItemStack removedItemStack)
    {
        if (mainWeaponItem == rightHandItem)
        {
            Destroy(rightHandItem.gameObject);
            rightHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, rightHandEquipPosition);
            mainWeaponItem = rightHandItem;
        }
        else if (mainWeaponItem == leftHandItem)
        {
            Destroy(leftHandItem.gameObject);
            leftHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, leftHandEquipPosition);
            secondaryWeaponItem = leftHandItem;
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
            rightHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, rightHandEquipPosition);
            mainWeaponItem = rightHandItem;
        }
        else if (secondaryWeaponItem == leftHandItem)
        {
            Destroy(leftHandItem.gameObject);
            leftHandItem = InstantiateItemInEquipmentPosition(unarmedHandItem, leftHandEquipPosition);
            secondaryWeaponItem = leftHandItem;
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

    Item InstantiateItemInEquipmentPosition(Item item, Transform eqpuipmentPosition)
    {
        var newItem = Instantiate(item, eqpuipmentPosition);
        newItem.transform.rotation = eqpuipmentPosition.rotation;
        newItem.GetComponent<Equipment>().SetItemEquiped(true);
        Destroy(newItem.GetComponent<PrefabRef>());
        Destroy(newItem.GetComponent<UniqueId>());

        var meleeWeapon = newItem.GetComponentInChildren<MeleeWeapon>();

        if (meleeWeapon)
            meleeWeapon.SetWeaponHolder(unit);

        var bow = newItem.GetComponent<Bow>();

        if (bow)
            animationEvents.SetBow(bow);

        return newItem;
    }

    public override bool CanAddItemStackInSlot(ItemStack itemStack, Slot slot)
    {
        return CanEquipTwoHandedWeapon(itemStack, slot);
    }

    // can't equip TwoHanded weapon if two one handed weapons equiped and there is no place for them in inventory or if secondary weapon slot is not empty
    bool CanEquipTwoHandedWeapon(ItemStack itemStack, Slot slot) 
    {
        if (slot == mainWeaponSlot &&
            !mainWeaponSlot.IsEmpty &&
            !secondaryWeaponSlot.IsEmpty &&
            itemStack.Item.Tags.Contains(ItemTag.TwoHanded) &&
            backpackSlots.Slots.All(i => !i.IsEmpty))
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
        var newItemStack = new ItemStack(itemStackToDivide.ItemPrefab, newItemStackItemsNumber);

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

    public override bool IsEmpty()
    {
        return mainWeaponSlot.IsEmpty &&
               secondaryWeaponSlot.IsEmpty &&
               quickSlots.IsEmpty && 
               BackpackSlots.IsEmpty;
    }
}
