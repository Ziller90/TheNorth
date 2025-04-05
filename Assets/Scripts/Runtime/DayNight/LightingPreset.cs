using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    [SerializeField] Gradient ambientColor;
    [SerializeField] Gradient moonLightColor;
    [SerializeField] Gradient sunLightColor;
    [SerializeField] Gradient contrastLightColor;
    [SerializeField] Vector3 contrastLightRotation;
    [SerializeField] Gradient fogColor;
    [SerializeField] AnimationCurve sunLightIntensityCurve;
    [SerializeField] AnimationCurve moonLightIntensityCurve;
    [SerializeField] AnimationCurve contrastLightIntensityCurve;
    [SerializeField] bool contrastLightShadows;

    public Gradient AmbientColor => ambientColor;
    public Gradient SunLightColor => sunLightColor;
    public Gradient MoonLightColor => moonLightColor;
    public Gradient ContrastLightColor => contrastLightColor;
    public Vector3 ContrastLightRotation => contrastLightRotation;
    public Gradient FogColor => fogColor;
    public AnimationCurve SunLightIntensityCurve => sunLightIntensityCurve;
    public AnimationCurve MoonLightIntensityCurve => moonLightIntensityCurve;
    public AnimationCurve ContrastLightIntensityCurve => contrastLightIntensityCurve;
    public bool ContrastLightShadows => contrastLightShadows;
}
