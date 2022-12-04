using UnityEngine;

public enum Control
{
    Mobile,
    MouseKeyboard,
    Gamepad
}
public class ControlType : MonoBehaviour
{
    [SerializeField] GameObject keyboardControl;
    [SerializeField] GameObject MobileIngameInterface;
    [SerializeField] bool useManuallySetControlType;
    [SerializeField] Control manuallySetControlType;
    Control currentControlType;

    void Start()
    {
        if (useManuallySetControlType == false)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                SetControlType(Control.Mobile);
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                SetControlType(Control.Mobile);
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                SetControlType(Control.MouseKeyboard);
            }
        }
        else
        {
            SetControlType(manuallySetControlType);
        }
    }
    public void SetControlType(Control controlType)
    {
        currentControlType = controlType;
        if (currentControlType == Control.Mobile)
        {
            keyboardControl.SetActive(false);
            MobileIngameInterface.SetActive(true);
        }
        if (currentControlType == Control.MouseKeyboard)
        {
            keyboardControl.SetActive(true);
            MobileIngameInterface.SetActive(false);
        }
        if (currentControlType == Control.Gamepad)
        {
            // behaviour when gamepad is connected
        }
    }
}
