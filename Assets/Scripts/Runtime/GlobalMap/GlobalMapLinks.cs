using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapLinks : MonoBehaviour
{
    public static GlobalMapLinks instance;
    public GlobalMapSquad playerSquad;
    public GameObject locationsInfoViewContainer;
    public Camera globalMapCamera;
    public SceneLauncherService gameSceneLauncher;
    private void Awake()
    {
        instance = this;
    }
}
