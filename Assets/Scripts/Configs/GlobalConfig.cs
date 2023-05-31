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
    ControlType currentControlType;
    public ControlType CurrentControlType => currentControlType;
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
