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
    [SerializeField] float cameraSwipeMovingSpeed;
    [SerializeField] float cameraWASDMovingSpeed;
    [SerializeField] float cameraZoomSpeed;
    [SerializeField] float cameraRotationSpeed;

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
        cameraManager.SetZoom(cameraManager.Zoom - Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed);
        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);

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
            Vector3 swipeMoveVector = cameraYRotation * new Vector3(mouseDragVector.x, 0, mouseDragVector.y) * cameraSwipeMovingSpeed;
            Debug.Log(" swipeMoveVector " +  swipeMoveVector);
            cameraManager.SetObservedPoint(fixedObservedPoint + swipeMoveVector);
        }
        if (Input.GetKey(KeyCode.Mouse1) && mode == ControlMode.Rotate)
        {
            Vector3 mouseDragVector = fixedScreenPoint - Input.mousePosition;
            float rotationAngle = mouseDragVector.x * cameraRotationSpeed;
            cameraManager.SetRotation(fixedCameraRotation + rotationAngle);
        }
        Vector3 WASDMoveVector = cameraYRotation * Utils.CalculateWASDVector() * cameraWASDMovingSpeed;
        if (WASDMoveVector.magnitude > 0)
            cameraManager.SetObservedPoint(cameraManager.ObservedPoint + WASDMoveVector);
    }
}
