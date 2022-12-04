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

    public void MeleeAttackPressed() { isMeleeAttackActing = true; }
    public void MeleeAttackReleased() { isMeleeAttackActing = false; }

    public void DistanceAttackPressed() { isDistantAttackActing = true; }
    public void DistanceAttackReleased() { isDistantAttackActing = false; }

    public void BlockPressed() { isBlockActing = true; }
    public void BlockReleased() { isBlockActing = false; }

    public void PickUpItemPressed()
    {
        Debug.Log("Yes");
        OnPickUpItemPressed(); 
    }
}
