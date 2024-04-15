using UnityEngine;
using SiegeUp.Core;

[System.Serializable, ComponentId(1)]
public class SimpleContainer : ContainerBase
{
    [AutoSerialize(1), SerializeField] SlotGroup slotGroup;
    public SlotGroup SlotGroup => slotGroup;
}
