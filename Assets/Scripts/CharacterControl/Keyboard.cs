using UnityEngine;
using UnityEngine.Events;


public class Keyboard : MonoBehaviour
{
    CameraFollowing camera;
    ControlManager controlManager;
    ActionManager actionManager;

    public UnityEvent openInventory;

    float vertical;
    float horizontal;
    Vector3 direction;

    private void Start()
    {
        camera = Links.instance.mainCamera.GetComponent<CameraFollowing>();
    }
    public void SetActionManager(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
    public void SetControlManager(ControlManager controlManager)
    {
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
            actionManager.OnOpenInventoryPressed();
        }
        if (Input.GetMouseButtonDown(0))
        {
            actionManager.mainWeaponUsing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            actionManager.mainWeaponUsing = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            actionManager.secondaryWeaponUsing = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            actionManager.secondaryWeaponUsing = false;

        }
    }
    public void MovePlayer()
    {
        Quaternion fixQuaternion = Quaternion.Euler(0, camera.CameraYRotation, 0);
        direction = fixQuaternion * ModelUtils.CalculateWASDVector();
        if (direction.magnitude > 0)
        {
            controlManager.SetControl(direction, MovingMode.Run);
        }
        else
        {
            controlManager.SetControl(direction, MovingMode.Stand);
        }
    }
}
