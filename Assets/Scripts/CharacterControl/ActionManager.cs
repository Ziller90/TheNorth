 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour
{
    public UnityAction OnOpenInventoryPressed;
    public UnityAction OnPickUpItemPressed;

    public bool isMeleeAttackActing;
    public bool isDistantAttackActing;
    public bool isBlockActing;
}
