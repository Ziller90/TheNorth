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
    public void RightHandPressed() { actionManager.rightHandWeaponUsing = true; }
    public void RightHandReleased() { actionManager.rightHandWeaponUsing = false; }

    public void DistanceAttackPressed() { actionManager.isDistantAttackActing = true; }
    public void DistanceAttackReleased() { actionManager.isDistantAttackActing = false; }

    public void BlockPressed() { actionManager.isBlockActing = true; }
    public void BlockReleased() { actionManager.isBlockActing = false; }

    public void PickUpItemPressed()
    {
        actionManager.OnInteractPressed();
    }
}
