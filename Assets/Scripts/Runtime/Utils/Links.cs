using SiegeUp.Core;
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
    public LocationModel locationModel;
    public GameSceneInitializer sceneInitializer;
    public GameObject playerCharacter;
    public LocationLoader locationLoader;
    public Keyboard keyboard;
    public FixedJoystick fixedJoystick;
    public MobileButtonsManager mobileButtonsManager;
    public DeathScreen deathScreen;
    public GlobalConfig globalConfig;
    public ItemsManagerWindow currentItemsViewManager;
    public LightingManager lightingManager;
    public SavingService savingService;
    public PrefabManager prefabManager;

    public void Awake()
    {
        instance = this;
    }
}
