using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] Light Sun;
    [SerializeField] Light Moon;
    [SerializeField] LightingPreset Preset;
    [SerializeField] GameTime gameTime;
    [SerializeField] bool isPlayerIndoors;

    void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
            UpdateLighting(gameTime.TimeOfDay / 24f);
        else
            UpdateLighting(gameTime.TimeOfDay / 24f);
    }

    public void SetPlayerInDoors(bool indoors)
    {
        Sun.gameObject.SetActive(!indoors);
        Moon.gameObject.SetActive(!indoors);
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (Sun != null)
        {
            Debug.Log(timePercent);
            Sun.color = Preset.SunLightColor.Evaluate(timePercent);
            Moon.color = Preset.MoonLightColor.Evaluate(timePercent);

            Sun.intensity = Preset.SunLightIntensityCurve.Evaluate(timePercent);
            Moon.intensity = Preset.MoonLightIntensityCurve.Evaluate(timePercent);

            Sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            Moon.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + 91f, 170f, 0));

            //Sun.enabled = Sun.intensity > Preset.IngorLightIntensity;
            //Moon.enabled = Moon.intensity > Preset.IngorLightIntensity;
        }
    }

    private void OnValidate()
    {
        if (Sun != null)
            return;

        if (RenderSettings.sun != null)
        {
            Sun = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    Sun = light;
                    return;
                }
            }
        }
    }
}
