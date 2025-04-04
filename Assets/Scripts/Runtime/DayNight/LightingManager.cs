using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] Light sun;
    [SerializeField] Light moon;
    [SerializeField] Light contrast;
    [SerializeField] LightingPreset preset;

    void Update()
    {
        if (preset)
            UpdateLighting(Game.TimeService.TimeOfDay / 24f);
    }

    public void SetLightingPreset(LightingPreset newPreset)
    {
        preset = newPreset; 
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (sun != null)
        {
            sun.color = preset.SunLightColor.Evaluate(timePercent);
            moon.color = preset.MoonLightColor.Evaluate(timePercent);
            contrast.color = preset.ContrastLightColor.Evaluate(timePercent);

            sun.intensity = preset.SunLightIntensityCurve.Evaluate(timePercent);
            moon.intensity = preset.MoonLightIntensityCurve.Evaluate(timePercent);
            contrast.intensity = preset.ContrastLightIntensityCurve.Evaluate(timePercent);

            sun.gameObject.SetActive(sun.intensity > 0.01f);
            moon.gameObject.SetActive(moon.intensity > 0.01f);
            contrast.gameObject.SetActive(contrast.intensity > 0.01f);

            sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            moon.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + 91f, 170f, 0));
            contrast.transform.localRotation = Quaternion.Euler(preset.ContrastLightRotation);
            contrast.shadows = preset.ContrastLightShadows ? LightShadows.Soft : LightShadows.None;
        }
    }

    private void OnValidate()
    {
        if (sun != null)
            return;

        if (RenderSettings.sun != null)
        {
            sun = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    sun = light;
                    return;
                }
            }
        }
    }
}
