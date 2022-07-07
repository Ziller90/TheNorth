using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtonsManager : MonoBehaviour
{
    public MobileButton meleeAttackButton;
    public MobileButton blockButton;
    public MobileButton distantAttackButton;
    public ActionManager buttonsManager;

    void Update()
    {
        buttonsManager.isMeleeAttackActing = meleeAttackButton.isPressed;
        buttonsManager.isDistantAttackActing = distantAttackButton.isPressed;
        buttonsManager.isBlockActing = blockButton.isPressed;
    }
}
