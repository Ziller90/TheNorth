using System;
using UnityEngine;

public class HumanoidInventory : MonoBehaviour
{
    [SerializeField] AnimationEvents animationEvents;
    [SerializeField] Creature creature;
    [SerializeField] Transform dropPosition;

    [SerializeField] Transform rightHandKeeper;
    [SerializeField] Transform leftHandKeeper;

    [SerializeField] Slot mainWeaponSlot;
    [SerializeField] Slot secondaryWeaponSlot;
    [SerializeField] Slot[] quickAccessSlots = new Slot[3];

    [SerializeField] Container dropSackPrefab;

    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] Container inventoryContainer;

    Item rightHandItem;
    Item leftHandItem;

    Item mainWeapon;
    Item secondaryWeapon;

    public Slot MainWeaponSlot => mainWeaponSlot;
    public Slot SecondaryWeaponSlot => secondaryWeaponSlot;
    public Slot[] QuickAccessSlots => quickAccessSlots;
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

        if (item.SuitableSlots == SlotType.MainWeapon && mainWeaponSlot.isEmpty)
        {
            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack, SlotType.MainWeapon);
        }
        else if (item.SuitableSlots == SlotType.SecondaryWeapon &&
            secondaryWeaponSlot.isEmpty && 
            (mainWeaponSlot.isEmpty || mainWeaponSlot.ItemStack.Item.SuitableSlots != SlotType.TwoHanded))
        {
            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack, SlotType.SecondaryWeapon);
        }
        else if (item.SuitableSlots == SlotType.TwoHanded && secondaryWeaponSlot.isEmpty && mainWeaponSlot.isEmpty)
        {
            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack, SlotType.MainWeapon);
        }
        else if (item.SuitableSlots == SlotType.BothHanded && mainWeaponSlot.isEmpty)
        {
            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack, SlotType.MainWeapon);
        }
        else if (item.SuitableSlots == SlotType.BothHanded &&
            secondaryWeaponSlot.isEmpty &&
            (mainWeaponSlot.isEmpty || mainWeaponSlot.ItemStack.Item.SuitableSlots != SlotType.TwoHanded))
        {
            RemoveItemFromWorld(item);
            SetItemStackInEquipmentPosition(newItemStack, SlotType.SecondaryWeapon);
        }
        else if (item.SuitableSlots == SlotType.QuikAcess && ModelUtils.TryAddOrMergeItemStackToSlotGroup(newItemStack, quickAccessSlots))
        {
            RemoveItemFromWorld(item);
        }
        else
        {
            AddItemToInventory(item, newItemStack);
        }
    }

    public void SetItemStackInEquipmentPosition(ItemStack itemStack, SlotType slot)
    {
        if (slot == SlotType.MainWeapon)
        {
            mainWeaponSlot.SetItemStack(itemStack);
        }
        else if (slot == SlotType.SecondaryWeapon)
        {
            secondaryWeaponSlot.SetItemStack(itemStack);
        }

        Item instantiatedItem = null;
        if (itemStack.Item.EquipPositon == EquipPositon.BothHand)
        {
            if (slot == SlotType.MainWeapon)
            {
                instantiatedItem = InstantiateItemInHand(itemStack, rightHandKeeper);
                rightHandItem = instantiatedItem;
            }
            else if (slot == SlotType.SecondaryWeapon)
            {
                instantiatedItem = InstantiateItemInHand(itemStack, leftHandKeeper);
                leftHandItem = instantiatedItem;
            }
        }

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
            if (item.SuitableSlots == SlotType.MainWeapon || item.SuitableSlots == SlotType.TwoHanded)
            {
                mainWeapon = item;
                battleSystem.SetMainWeapon(item.GetComponent<Weapon>());
            }
            if (item.SuitableSlots == SlotType.SecondaryWeapon)
            {
                secondaryWeapon = item;
                battleSystem.SetSecondaryWeapon(item.GetComponent<Weapon>());
            }
            if (item.SuitableSlots == SlotType.BothHanded)
            {
                if (item == rightHandItem)
                {
                    mainWeapon = item;
                    battleSystem.SetMainWeapon(item.GetComponent<Weapon>());
                }
                if (item == leftHandItem)
                {
                    secondaryWeapon = item;
                    battleSystem.SetSecondaryWeapon(item.GetComponent<Weapon>());
                }
            }
        }
    }

    public  void AddItemToInventory(Item item, ItemStack itemStack)
    {
        bool added = ModelUtils.TryAddOrMergeItemStackToSlotGroup(itemStack, inventoryContainer.Slots);
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

    public void DropItemsStackFromSlot(Slot slot)
    {
        var dropSack = Instantiate(dropSackPrefab, dropPosition.position, Quaternion.identity);
        ModelUtils.TryAddOrMergeItemStackToSlotGroup(slot.ItemStack, dropSack.Slots);
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

    public void RemoveMainWeapon()
    {
        if (mainWeapon == rightHandItem)
            RemoveEquipment(EquipPositon.RightHand);
        if (mainWeapon == leftHandItem)
            RemoveEquipment(EquipPositon.LeftHand);

        mainWeaponSlot.SetEmpty();
    }

    public void RemoveSecondaryWeapon()
    {
        if (secondaryWeapon == rightHandItem)
            RemoveEquipment(EquipPositon.RightHand);
        if (secondaryWeapon == leftHandItem)
            RemoveEquipment(EquipPositon.LeftHand);

        secondaryWeaponSlot.SetEmpty();
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
        quickAccessSlots[index].SetItemStack(itemsStack);
    }

    public void RemoveItemFromQuickAccessSlot(int index)
    {
        quickAccessSlots[index].SetEmpty();
    }
}
