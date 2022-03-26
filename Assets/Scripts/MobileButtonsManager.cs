using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtonsManager : MonoBehaviour
{
    public MobileButton meleeAttackButton;
    public MobileButton blockButton;
    public MobileButton distantAttackButton;
    public ButtonsManager buttonsManager;

    void Start()
    {
        
    }

    void Update()
    {
        buttonsManager.isMeleeAttackButtonPressed = meleeAttackButton.isPressed;
        buttonsManager.isDistantAttackButtonPressed = distantAttackButton.isPressed;
        buttonsManager.isBlockButtonPressed = blockButton.isPressed;
    }
}
