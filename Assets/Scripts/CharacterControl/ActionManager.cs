 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ActionManager : MonoBehaviour
{
    public UnityAction OnMeleeAttackHold;
    public UnityAction OnDistanceAttackHold;
    public UnityAction OnBlockHold;
    public UnityAction OnOpenInventoryPressed;
    public UnityAction OnPickUpItemPressed;
    public void MeleeAttackHold()
    {
        OnMeleeAttackHold();
    }
    public void DistanceAttackHold()
    {
        OnDistanceAttackHold();
    }
    public void BlockHold()
    {
        OnBlockHold();
    }
    public void OpenInventoryPressed()
    {
        OnOpenInventoryPressed();
    }
    public void PickUpItemPressed()
    {
        OnPickUpItemPressed();
    }

    [HideInInspector] public bool isMeleeAttackActing;
    [HideInInspector] public bool isDistantAttackActing;
    [HideInInspector] public bool isBlockActing;
}
