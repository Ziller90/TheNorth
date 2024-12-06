using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System.Linq;

[System.Serializable]
public class EquipBestItems : ActionNode
{
    HumanoidInventoryContainer AIInventory;

    [SerializeField] NodeProperty<Slot.SlotType> slotType = new NodeProperty<Slot.SlotType>();
    [SerializeField] NodeProperty<ItemTag> itemTag = new NodeProperty<ItemTag>();

    protected override State OnUpdate()
    {
        AIInventory = context.GameObject.GetComponent<HumanoidInventoryContainer>();

        if (slotType.Value == Slot.SlotType.MainWeaponSlot)
        {
            EquipMostExpensiveItemSuitableForSlot(AIInventory.MainWeaponSlot, itemTag.Value);
        }
        else if (slotType.Value == Slot.SlotType.SecondaryWeaponSlot)
        {
            EquipMostExpensiveItemSuitableForSlot(AIInventory.SecondaryWeaponSlot, itemTag.Value);
        }

        return State.Success;
    }

    void EquipMostExpensiveItemSuitableForSlot(Slot slot, ItemTag itemTag)
    {
        var suitableSlots = AIInventory.BackpackSlots.Slots.Where(i => !i.IsEmpty && slot.IsSuitable(i.ItemStack) && i.ItemStack.Item.Tags.Contains(itemTag));
        var mostExpensiveItemSlot = suitableSlots.OrderByDescending(i => i.ItemStack.Item.Cost).FirstOrDefault();

        if (mostExpensiveItemSlot != null)
            ModelUtils.TryMoveFromSlotToSlot(AIInventory, mostExpensiveItemSlot, AIInventory, slot);
    }
}
