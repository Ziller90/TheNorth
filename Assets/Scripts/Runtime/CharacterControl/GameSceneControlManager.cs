using UnityEngine;

public class GameSceneControlManager : MonoBehaviour
{
    [SerializeField] GameObject keyboardControl;
    [SerializeField] GameObject MobileIngameInterface;
    [SerializeField] GlobalConfig globalConfig;
    void Start()
    {
        SetControlType(globalConfig.CurrentControlType);
    }
    public void SetControlType(ControlType controlType)
    {
        if (controlType == ControlType.Mobile)
        {
            keyboardControl.SetActive(false);
            MobileIngameInterface.SetActive(true);
        }
        if (controlType == ControlType.PC)
        {
            keyboardControl.SetActive(true);
            MobileIngameInterface.SetActive(false);
        }
        if (controlType == ControlType.Gamepad)
        {
            // behaviour when gamepad is connected
        }
    }
}
