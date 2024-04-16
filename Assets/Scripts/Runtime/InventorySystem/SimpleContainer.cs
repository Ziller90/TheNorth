using UnityEngine;
using SiegeUp.Core;

[System.Serializable, ComponentId(1)]
public class SimpleContainer : ContainerBase
{
    [AutoSerialize(1), SerializeField] SlotGroup slotGroup = new();
    public SlotGroup SlotGroup => slotGroup;

    public void InitializeSlotGroup(int slotGroupSize)
    {
        slotGroup.InitializeSlotsGroup(slotGroupSize);
    }

    public void SetSlotGroup(SlotGroup slotGroup)
    {
        this.slotGroup = slotGroup;
    }

    public override bool IsEmpty() => slotGroup.IsEmpty;
}
