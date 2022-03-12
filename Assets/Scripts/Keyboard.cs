using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public Vector3 direction;
    public CameraFollowing camera;
    public ControlManager controlManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.S)))
        {
            vertical = 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }
        else if  (Input.GetKey(KeyCode.S))
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
        controlManager.SetControl(direction, direction.magnitude);

    }
}
