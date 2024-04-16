using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerBase : MonoBehaviour
{
    public virtual bool CanAddItemStackInSlot(ItemStack itemStack, Slot slot)
    {
        return true;
    }

    public virtual bool CanAddItemStackInSlotGroup(ItemStack itemStack, SlotGroup slotGroup)
    {
        return true;
    }

    public virtual bool CanRemoveItemStackFromSlot(Slot slot)
    {
        return true;
    }

    public abstract bool IsEmpty();
}
