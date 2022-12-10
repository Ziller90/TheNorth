using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtonsManager : MonoBehaviour
{
    ActionManager actionManager;
    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
    public void MeleeAttackPressed() { actionManager.isMeleeAttackActing = true; }
    public void MeleeAttackReleased() { actionManager.isMeleeAttackActing = false; }

    public void DistanceAttackPressed() { actionManager.isDistantAttackActing = true; }
    public void DistanceAttackReleased() { actionManager.isDistantAttackActing = false; }

    public void BlockPressed() { actionManager.isBlockActing = true; }
    public void BlockReleased() { actionManager.isBlockActing = false; }

    public void PickUpItemPressed()
    {
        actionManager.OnPickUpItemPressed();
    }
}
