using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class Container : MonoBehaviour
{
    [SerializeField] Slot[] containerSlots;
    public Slot[] Slots => containerSlots;

    public void TryAddOrMergeItemStackToContainer(ItemStack itemStack)
    {
        ModelUtils.TryAddOrMergeItemStackToSlotGroup(itemStack, containerSlots);
    }
}
