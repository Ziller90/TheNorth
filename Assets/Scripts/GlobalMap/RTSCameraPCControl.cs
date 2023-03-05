using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraPCControl : MonoBehaviour
{
    enum ControlMode
    {
        None,
        Move,
        Rotate
    }
    [SerializeField] float swipeMovingSpeed;
    [SerializeField] float WASDMovingSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float rotationSpeed;

    Vector3 fixedScreenPoint;
    Vector3 fixedObservedPoint;
    float fixedCameraRotation;
    RTSCamera cameraManager;
    ControlMode mode = ControlMode.None;
    void Start()
    {
        cameraManager = gameObject.GetComponent<RTSCamera>();
    }
    void Update()
    {
        cameraManager.SetZoom(cameraManager.Zoom - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);

        Vector3 WASDMoveVector = cameraYRotation * Utils.CalculateWASDVector() * WASDMovingSpeed;
        if (WASDMoveVector.magnitude > 0)
            cameraManager.SetObservedPoint(cameraManager.ObservedPoint + WASDMoveVector);

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && mode == ControlMode.None) 
        {
            fixedScreenPoint = Input.mousePosition;
            fixedObservedPoint = cameraManager.ObservedPoint;
            fixedCameraRotation = cameraManager.RotationAngle;
            if (Input.GetKeyDown(KeyCode.Mouse0))
                mode = ControlMode.Move;
            if (Input.GetKeyDown(KeyCode.Mouse1))
                mode = ControlMode.Rotate;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && mode == ControlMode.Move)
            mode = ControlMode.None;
        if (Input.GetKeyUp(KeyCode.Mouse1) && mode == ControlMode.Rotate)
            mode = ControlMode.None;

        if (Input.GetKey(KeyCode.Mouse0) && mode == ControlMode.Move) 
        {
            Vector3 mouseDragVector = fixedScreenPoint - Input.mousePosition;
            Vector3 swipeMoveVector = cameraYRotation * new Vector3(mouseDragVector.x, 0, mouseDragVector.y) * swipeMovingSpeed;
            cameraManager.SetObservedPoint(fixedObservedPoint + swipeMoveVector);
        }
        if (Input.GetKey(KeyCode.Mouse1) && mode == ControlMode.Rotate)
        {
            Vector3 mouseDragVector = fixedScreenPoint - Input.mousePosition;
            float rotationAngle = mouseDragVector.x * rotationSpeed;
            cameraManager.SetRotation(fixedCameraRotation + rotationAngle);
        }
    }
}
