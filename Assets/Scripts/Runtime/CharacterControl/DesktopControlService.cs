using UnityEngine;
using UnityEngine.Events;


public class DesktopControlService : MonoBehaviour
{
    public UnityEvent openInventory;

    CameraControlService mainCameraService;
    ControlManager controlManager;
    ActionManager actionManager;
    Vector3 direction;

    private void Start() => mainCameraService = Game.CameraControlService;

    public void SetControl(ControlManager controlManager, ActionManager actionManager)
    {
        this.actionManager = actionManager;
        this.controlManager = controlManager;
    }

    void Update()
    {
        MovePlayer();
        ListenKeyboardButtons();
    }

    public void ListenKeyboardButtons()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventory?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            actionManager.onOpenInventoryPressed();
        }
        if (Input.GetMouseButtonDown(0))
        {
            actionManager.MainWeaponPressed();
        }
        if (Input.GetMouseButtonUp(0))
        {
            actionManager.MainWeaponReleased();
        }
        if (Input.GetMouseButtonDown(1))
        {
            actionManager.SecondaryWeaponPressed();
        }
        if (Input.GetMouseButtonUp(1))
        {
            actionManager.SecondaryWeaponReleased();
        }
    }

    public void MovePlayer()
    {
        Quaternion fixQuaternion = Quaternion.Euler(0, mainCameraService.CameraYRotation, 0);
        direction = fixQuaternion * ModelUtils.CalculateWASDVector();

        if (direction.magnitude > 0)
            controlManager.SetControl(direction, MovingMode.Run);
        else
            controlManager.SetControl(direction, MovingMode.Stand);
    }
}
