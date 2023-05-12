using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Links : MonoBehaviour
{
    public static Links instance;

    public GlobalLists globalLists;
    public GameObject mainCamera;
    public Transform healthBarsContainer;
    public LocationManager locationSettings;
    public GameSceneInitializer sceneInitializer;
    public GameObject playerCharacter;
    public LocationLoader locationLoader;
    public Keyboard keyboard;
    public FixedJoystick fixedJoystick;
    public MobileButtonsManager mobileButtonsManager;
    public DeathScreen deathScreen;

    public void Awake()
    {
        instance = this;
    }
}
