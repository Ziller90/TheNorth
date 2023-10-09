using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType
{
    Mobile,
    PC,
    Gamepad
}

[CreateAssetMenu(fileName = "New global config", menuName = "Global config")]
public class GlobalConfig : ScriptableObject
{
    [SerializeField] ControlType manuallySelectedControlType;
    [SerializeField] bool allowAutoControlTypeSelect;

    [SerializeField] Material hightlightMaterial;
    [SerializeField] AnimationCurve highlightCurve;
    [SerializeField] AnimationCurve fadeOutCurve;
    [SerializeField] float highlighSpeed;
    [SerializeField] float fadeOutSpeed;

    [ColorUsage(false, true)] [SerializeField] Color32 highLightColor;
    ControlType currentControlType;
    public ControlType CurrentControlType => currentControlType;
    public Material HightlightMaterial => hightlightMaterial;
    public AnimationCurve HighlightCurve => highlightCurve;
    public float HighlighSpeed => highlighSpeed;
    public float FadeOutSpeed => fadeOutSpeed;
    public AnimationCurve FadeOutCurve => fadeOutCurve;
    public Color32 HighLightColor => highLightColor;


    public void SetControlType()
    {
        if (allowAutoControlTypeSelect)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                currentControlType = ControlType.Mobile;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                currentControlType = ControlType.Mobile;
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                currentControlType = ControlType.PC;
            }
            if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                currentControlType = ControlType.PC;
            }
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                currentControlType = manuallySelectedControlType;
            }
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                currentControlType = manuallySelectedControlType;
            }
        }
        else
        {
            currentControlType = manuallySelectedControlType;
        }
    }

    public void OnEnable()
    {
        SetControlType();
    }
}
