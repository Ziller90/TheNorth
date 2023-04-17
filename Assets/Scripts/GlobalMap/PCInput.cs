using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : MonoBehaviour
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

    [SerializeField] float minClickDistanceForClick;
    
    ScreenClicksManager screenClicksManager;
    bool clickRequested;
    Vector2 startClickPosition;
    Vector3 fixedScreenPoint;
    Vector3 fixedObservedPoint;
    float fixedCameraRotation;
    RTSCamera cameraManager;
    ControlMode mode = ControlMode.None;
    void Start()
    {
        cameraManager = GetComponent<RTSCamera>();
        screenClicksManager = GetComponent<ScreenClicksManager>();
    }
    void Update()
    {
        cameraManager.SetZoom(cameraManager.Zoom - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);

        Vector3 WASDMoveVector = cameraYRotation * Utils.CalculateWASDVector() * WASDMovingSpeed;
        WASDMoveVector *= (cameraManager.Zoom + 1) * cameraManager.ZoomCameraSpeedModifier;
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
            swipeMoveVector *= (cameraManager.Zoom + 1) * cameraManager.ZoomCameraSpeedModifier;
            cameraManager.SetObservedPoint(fixedObservedPoint + swipeMoveVector);
        }
        if (Input.GetKey(KeyCode.Mouse1) && mode == ControlMode.Rotate)
        {
            Vector3 mouseDragVector = fixedScreenPoint - Input.mousePosition;
            float rotationAngle = mouseDragVector.x * rotationSpeed;
            cameraManager.SetRotation(fixedCameraRotation + rotationAngle);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickRequested = true;
            startClickPosition = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && clickRequested == true)
        {
            float clickDistance = Vector2.Distance(startClickPosition, Input.mousePosition);
            if (clickDistance < minClickDistanceForClick)
                screenClicksManager.ScreenClick(Input.mousePosition);
            clickRequested = false;
        }
    }
}
