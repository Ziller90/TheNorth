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

    public void MainWeaponPressed() => actionManager.MainWeaponPressed();

    public void MainWeaponReleased() => actionManager.MainWeaponReleased();

    public void SecondaryWeaponPressed() { actionManager.secondaryWeaponUsing = true; }
    public void SecondaryWeaponReleased() { actionManager.secondaryWeaponUsing = false; }

    public void PickUpItemPressed()
    {
        actionManager.onInteractPressed();
    }


}
