using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] CameraFollowing camera;
    [SerializeField] ControlManager controlManager;
    [SerializeField] ActionManager actionManager;

    float vertical;
    float horizontal;
    Vector3 direction;

    void Update()
    {
        MovePlayer();
        ListenKeyboardButtons();
    }
    public void ListenKeyboardButtons()
    {
        if (Input.GetKeyDown("I"))
        {
            actionManager.OpenInventoryPressed();
        }
        if (Input.GetKeyDown("E"))
        {
            actionManager.PickUpItemPressed();
        }
        if (Input.GetMouseButton(0))
        {
            actionManager.MeleeAttackHold();
        }
        if (Input.GetKey("Q"))
        {
            actionManager.DistanceAttackHold();
        }
        if (Input.GetMouseButton(1))
        {
            actionManager.BlockHold();
        }
    }
    public void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.S)))
        {
            vertical = 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }

        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.D)))
        {
            horizontal = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }
        direction = Utils.GetDirection(horizontal, vertical, camera.cameraYRotation);
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
