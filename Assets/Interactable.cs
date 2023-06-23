using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> meshRenderers;
    [SerializeField] bool isInteractable = true;
    AnimationCurve highlightCurve; 
    AnimationCurve fadeOutCurve;
    Color32 highlightEmissionColor;
    Color32 defaultEmissionColor;

    float highlightSpeed;
    bool isHighlighted;

    float startTime;
    float currentPhase;
    float fadeOutStartTime = 0;
    float fadeOutSpeed;
    float fadeOutStartingPhase;
    public bool IsInteractable { get; set; }

    private void Start()
    {
        highlightCurve = Links.instance.globalConfig.HighlightCurve;
        fadeOutCurve = Links.instance.globalConfig.FadeOutCurve;
        highlightSpeed = Links.instance.globalConfig.HighlighSpeed;
        highlightEmissionColor = Links.instance.globalConfig.HighLightColor;
        fadeOutSpeed = Links.instance.globalConfig.FadeOutSpeed;
        defaultEmissionColor = new Color32(0, 0, 0, 0);
    }
    void OnEnable()
    {
        if (isInteractable)
            Links.instance.globalLists.AddInteractableOnLocation(this);
    }
    void OnDisable()
    {
        if (isInteractable)
            Links.instance.globalLists.RemoveInteractableOnLocation(this);
    }
    public void SetHighlighted(bool highlight)
    {
        if (highlight && !isHighlighted)
        {
            isHighlighted = true;
            startTime = Time.time;
        }
        else if (!highlight && isHighlighted)
        {
            StartCoroutine(DisableHighlighting());
        }
    }
    public void Update()
    {
        if (isHighlighted)
        {
            if (fadeOutStartTime == 0)
                currentPhase = GetPhase();
            else
                currentPhase = GetFadeOutPhase();

            Color32 currentEmissionColor = Color.Lerp(defaultEmissionColor, highlightEmissionColor, currentPhase);
            foreach (var renderer in meshRenderers)
            {
                renderer.material.SetColor("_EmissionColor", currentEmissionColor);
            }
        }
    }
    IEnumerator DisableHighlighting()
    {
        fadeOutStartingPhase = currentPhase;
        fadeOutStartTime = Time.time;
        while(currentPhase > 0.001f)
        {
            yield return new WaitForEndOfFrame();
        }
        fadeOutStartTime = 0;
        currentPhase = 0;
        isHighlighted = false;
    }
    float GetPhase()
    {
        return highlightCurve.Evaluate((Time.time - startTime) * highlightSpeed);
    }
    float GetFadeOutPhase()
    {
        return (fadeOutCurve.Evaluate((Time.time - fadeOutStartTime) * fadeOutSpeed) * fadeOutStartingPhase);
    }
}
