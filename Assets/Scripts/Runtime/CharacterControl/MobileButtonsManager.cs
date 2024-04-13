using UnityEngine;

public class MobileControlService : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;

    [SerializeField] float minJoysticNormalizedMagnitudeToWalk; // Minimal speed modificator value when character starts walking
    [SerializeField] float minJoysticNormalizedMagnitudeToRun; // Minimal speed modificator value when character starts runing

    ActionManager actionManager;
    ControlManager controlManager;

    public void SetControl(ControlManager controlManager, ActionManager actionManager)
    {
        this.actionManager = actionManager;
        this.controlManager = controlManager;
    }

    void Update()
    {
        var moveMagnitude = fixedJoystick.Direction.magnitude;
        MovingMode moveMode;

        if (moveMagnitude > minJoysticNormalizedMagnitudeToRun)
        {
            moveMode = MovingMode.Run;
        }
        else if (moveMagnitude > minJoysticNormalizedMagnitudeToWalk)
        {
            moveMode = MovingMode.Walk;
        }
        else
        {
            moveMode = MovingMode.Stand;
        }

        controlManager.SetControl(fixedJoystick.Direction, moveMode);
    }

    public void MainWeaponPressed() => actionManager.MainWeaponPressed();

    public void MainWeaponReleased() => actionManager.MainWeaponReleased();

    public void SecondaryWeaponPressed() => actionManager.SecondaryWeaponPressed();
    public void SecondaryWeaponReleased() => actionManager.SecondaryWeaponReleased();

    public void PickUpItemPressed()
    {
        actionManager.onInteractPressed();
    }


}
