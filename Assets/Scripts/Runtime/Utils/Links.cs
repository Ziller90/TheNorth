using SiegeUp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Links : MonoBehaviour
{
    public static Links instance;

    public Keyboard keyboard;
    public MobileButtonsManager mobileButtonsManager;
    public FixedJoystick fixedJoystick;

    public ItemsManagerWindow currentItemsViewManager;
    public DeathScreen deathScreen;
    public Transform healthBarsContainer;

    public void Awake()
    {
        instance = this;
    }
}
