using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using SiegeUp.Core;

[DebuggerStepThrough]
public static class Game
{
    public static ActorsAccessModel ActorsAccessModel => Service<ActorsAccessModel>.Instance;
    public static LocationLoader LocationLoader => Service<LocationLoader>.Instance;
    public static MainCameraService MainCameraService => Service<MainCameraService>.Instance;
    public static LightingManager LightingManager => Service<LightingManager>.Instance;
    public static GameSceneInitializer GameSceneInitializer => Service<GameSceneInitializer>.Instance;
    public static SavingService SavingService => Service<SavingService>.Instance;
    public static PrefabManager PrefabManager => Service<PrefabManager>.Instance;   
    public static GlobalConfig GlobalConfig => Service<GlobalConfig>.Instance;   
    public static WindowManagerView WindowManagerView => Service<WindowManagerView>.Instance;   
    public static FactionsConfig FactionsConfig => Service<FactionsConfig>.Instance;   
}
