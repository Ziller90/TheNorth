using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

enum HighlightState
{
    None = 0,
    Highlighting = 1,
    FadingOut = 2
}
public class InteractableObjectView : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> meshRenderers = new();
    [SerializeField] List<SkinnedMeshRenderer> skinnedMeshRenderers = new();
    [SerializeField] InteractableObject interactableObject;

    AnimationCurve highlightCurve; 
    AnimationCurve fadeOutCurve;
    Color32 highlightEmissionColor;
    Color32 defaultEmissionColor;
    HighlightState currentState;
    float highlightSpeed;
    bool isHighlighted;
    float startTime;
    float currentPhase;
    float fadeOutStartTime = 0;
    float fadeOutSpeed;
    float fadeOutStartingPhase;

    private void Awake()
    {
        highlightCurve = Game.GlobalConfig.HighlightCurve;
        fadeOutCurve = Game.GlobalConfig.FadeOutCurve;
        highlightSpeed = Game.GlobalConfig.HighlighSpeed;
        highlightEmissionColor = Game.GlobalConfig.HighLightColor;
        fadeOutSpeed = Game.GlobalConfig.FadeOutSpeed;
        defaultEmissionColor = new Color32(0, 0, 0, 0);
    }

    public void SetInteractableObject(InteractableObject interactableObject)
    {
        this.interactableObject = interactableObject;
        this.interactableObject.updateSelectionStateEvent += SetHighlighted;
    }

    void OnEnable()
    {
        if (interactableObject)
            interactableObject.updateSelectionStateEvent += SetHighlighted;
    }

    void OnDisable()
    {
        if (interactableObject)
            interactableObject.updateSelectionStateEvent -= SetHighlighted;
    }

    [ContextMenu("Find Object's MeshRenderers")]
    public void FindObjectMeshRenderers()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
        skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
    }

    public void SetHighlighted(bool highlight)
    {
        if (currentState == HighlightState.None && highlight)
        {
            isHighlighted = true;
            currentState = HighlightState.Highlighting;
            startTime = Time.time;
        }
        if (currentState == HighlightState.FadingOut && highlight)
        {
            isHighlighted = true;
        }
        if (currentState == HighlightState.Highlighting && !highlight)
        {
            isHighlighted = false;
            fadeOutStartingPhase = currentPhase;
            fadeOutStartTime = Time.time;
            currentState = HighlightState.FadingOut;
        }
        if (currentState == HighlightState.FadingOut && !highlight)
        {
            isHighlighted = false;
        }
    }

    public void Update()
    {
        HighlightObject();
    }

    public void HighlightObject()
    {
        if (currentState == HighlightState.Highlighting)
            currentPhase = GetPhase();
        if (currentState == HighlightState.FadingOut)
            currentPhase = GetFadeOutPhase();
        if (currentState == HighlightState.None)
            currentPhase = 0;

        Color32 currentEmissionColor = Color.Lerp(defaultEmissionColor, highlightEmissionColor, currentPhase);
        foreach (var renderer in meshRenderers)
        {
            renderer.material.SetColor("_EmissionColor", currentEmissionColor);
        }

        foreach (var renderer in skinnedMeshRenderers)
        {
            renderer.material.SetColor("_EmissionColor", currentEmissionColor);
        }

        if (currentState == HighlightState.FadingOut && currentPhase < 0.001f)
        {
            fadeOutStartTime = 0;
            if (isHighlighted)
            {
                currentState = HighlightState.Highlighting;
                startTime = Time.time;
            }
            else
                currentState = HighlightState.None;
        }
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
