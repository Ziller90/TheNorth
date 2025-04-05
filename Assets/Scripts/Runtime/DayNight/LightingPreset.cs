using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    [SerializeField] Gradient ambientColor;
    [SerializeField] Gradient moonLightColor;
    [SerializeField] Gradient sunLightColor;
    [SerializeField] Gradient fogColor;
    [SerializeField] AnimationCurve sunLightIntensityCurve;
    [SerializeField] AnimationCurve moonLightIntensityCurve;

    public Gradient AmbientColor => ambientColor;
    public Gradient SunLightColor => sunLightColor;
    public Gradient MoonLightColor => moonLightColor;
    public Gradient FogColor => fogColor;
    public AnimationCurve SunLightIntensityCurve => sunLightIntensityCurve;
    public AnimationCurve MoonLightIntensityCurve => moonLightIntensityCurve;
}
