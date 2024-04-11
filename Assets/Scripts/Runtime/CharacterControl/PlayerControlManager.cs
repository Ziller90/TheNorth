using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    [SerializeField] Keyboard keyboardControl;
    [SerializeField] GameObject MobileIngameInterface;
    [SerializeField] GlobalConfig globalConfig;

    public Keyboard KeyboardControl => keyboardControl;    

    void Start()
    {
        SetControlType(globalConfig.CurrentControlType);
    }

    public void SetControlType(ControlType controlType)
    {
        if (controlType == ControlType.Mobile)
        {
            keyboardControl.gameObject.SetActive(false);
            MobileIngameInterface.SetActive(true);
        }
        if (controlType == ControlType.PC)
        {
            keyboardControl.gameObject.SetActive(true);
            MobileIngameInterface.SetActive(false);
        }
        if (controlType == ControlType.Gamepad)
        {
            // behaviour when gamepad is connected
        }
    }
}
