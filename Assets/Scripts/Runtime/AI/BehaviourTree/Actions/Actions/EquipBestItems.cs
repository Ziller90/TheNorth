using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System.Linq;

[System.Serializable]
public class EquipBestItems : ActionNode
{
    HumanoidInventoryContainer AIInventory;
    protected override State OnUpdate()
    {
        AIInventory = context.GameObject.GetComponent<HumanoidInventoryContainer>();
        EquipMostExpensiveItemSuitableForSlot(AIInventory.MainWeaponSlot);
        EquipMostExpensiveItemSuitableForSlot(AIInventory.SecondaryWeaponSlot);

        return State.Success;
    }

    void EquipMostExpensiveItemSuitableForSlot(Slot slot)
    {
        var suitableSlots = AIInventory.BackpackSlots.Slots.Where(i => !i.isEmpty && slot.IsSuitable(i.ItemStack));
        var mostExpensiveItemSlot = suitableSlots.OrderByDescending(i => i.ItemStack.Item.Cost).FirstOrDefault();

        if (mostExpensiveItemSlot != null)
            ModelUtils.TryMoveFromSlotToSlot(AIInventory, mostExpensiveItemSlot, AIInventory, slot);
    }
}
