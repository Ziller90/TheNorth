using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class SimpleContainer : ContainerBase
{
    [SerializeField] SlotGroup slotGroup;
    public SlotGroup SlotGroup => slotGroup;
}
