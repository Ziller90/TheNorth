using System.Collections;
using System.Collections.Generic;
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
        actionManager = Links.instance.playerActionManager;
        controlManager = Links.instance.playerControlManager;
        camera = Links.instance.mainCamera.GetComponent<CameraFollowing>();
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
            actionManager.PickUpItemPressed();
        }
        if (Input.GetMouseButtonDown(0))
        {
            actionManager.MeleeAttackPressed();
        }        
        if (Input.GetMouseButtonUp(0))
        {
            actionManager.MeleeAttackReleased();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            actionManager.DistanceAttackPressed();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            actionManager.DistanceAttackReleased();
        }

        if (Input.GetMouseButtonDown(1))
        {
            actionManager.BlockPressed();
        }
        if (Input.GetMouseButtonUp(1))
        {
            actionManager.BlockReleased();
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
