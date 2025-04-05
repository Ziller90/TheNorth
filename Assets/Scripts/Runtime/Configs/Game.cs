using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using SiegeUp.Core;

public static class Game
{
    public static ActorsAccessModel ActorsAccessModel => Service<ActorsAccessModel>.Instance;
    public static LocationLoader LocationLoader => Service<LocationLoader>.Instance;
    public static CameraControlService CameraControlService => Service<CameraControlService>.Instance;
    public static LightingManager LightingManager => Service<LightingManager>.Instance;
    public static GameSceneInitializer GameSceneInitializer => Service<GameSceneInitializer>.Instance;
    public static SavingService SavingService => Service<SavingService>.Instance;
    public static PrefabManager PrefabManager => Service<PrefabManager>.Instance;   
    public static ScriptableObjectManager ScriptableObjectManager => Service<ScriptableObjectManager>.Instance;   
    public static GlobalConfig GlobalConfig => Service<GlobalConfig>.Instance;   
    public static WindowManagerView WindowManagerView => Service<WindowManagerView>.Instance;   
    public static FactionsConfig FactionsConfig => Service<FactionsConfig>.Instance;   
    public static DesktopControlService DesktopControlService => Service<DesktopControlService>.Instance;   
    public static MobileControlService MobileControlService => Service<MobileControlService>.Instance;   
    public static MusicService MusicService => Service<MusicService>.Instance;   
    public static ButtonsSoundManager ButtonsSoundManager => Service<ButtonsSoundManager>.Instance;   
    public static SoundService SoundService => Service<SoundService>.Instance;   
    public static SceneLauncherService SceneLauncherService => Service<SceneLauncherService>.Instance;   
    public static TimeService TimeService => Service<TimeService>.Instance;   
}
