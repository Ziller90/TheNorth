using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSCamera))]
public class MobileInput : MonoBehaviour
{
    [SerializeField] float cameraOneTouchMovingSpeed;
    [SerializeField] float cameraTwoTouchesMovingSpeed;

    [SerializeField] float cameraZoomSpeed;
    [SerializeField] float cameraRotationSpeed;

    [SerializeField] float minTouchDistanceForTap;

    Vector2 oldTouchPosition;
    Vector2 oldTwoTouchesVector;
    Vector2 oldTwoTouchesMiddlePosition;
    float oldZoomTouchesDistance;

    bool movingOnPreviousFrame = false;
    bool zoomingOnPreviousFrame = false;

    RTSCamera cameraManager;
    ScreenClicksManager screenClicksManager;

    bool tapRequested;
    Vector2 startTapPosition;
    void Start()
    {
        cameraManager = GetComponent<RTSCamera>();
        screenClicksManager = GetComponent<ScreenClicksManager>();
    }
    void Update()
    {
        if (Input.touchCount == 2)
        {
            tapRequested = false;
            if (!zoomingOnPreviousFrame)
            {
                oldZoomTouchesDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                oldTwoTouchesVector = Input.GetTouch(0).position - Input.GetTouch(1).position;
                oldTwoTouchesMiddlePosition = Vector2.Lerp(Input.GetTouch(0).position, Input.GetTouch(1).position, 0.5f);
                zoomingOnPreviousFrame = true;
            }
            cameraManager.SetObservedPoint(cameraManager.ObservedPoint + GetMoveVectorByTwoTouches());
            cameraManager.SetRotation(cameraManager.RotationAngle + GetRotationAngle());
            cameraManager.SetZoom(cameraManager.Zoom + GetZoomModifier());
        }
        else
        {
            zoomingOnPreviousFrame = false;
        }

        if (Input.touchCount == 1)
        {
            TryCatchTap();

            if (!movingOnPreviousFrame)
            {
                oldTouchPosition = Input.GetTouch(0).position;
                movingOnPreviousFrame = true;
            }
            cameraManager.SetObservedPoint(cameraManager.ObservedPoint + GetMoveVectorByTouch());
        }
        else
        {
            movingOnPreviousFrame = false;
        }
    }
    void TryCatchTap()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapRequested = true;
            startTapPosition = Input.GetTouch(0).position;
        }
        if ((Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended) && tapRequested == true)
        {
            if (Vector2.Distance(startTapPosition, Input.GetTouch(0).position) < minTouchDistanceForTap)
            {
                screenClicksManager.ScreenClick(Input.GetTouch(0).position);
            }
            tapRequested = false;
        }
    }
    Vector3 GetMoveVectorByTwoTouches()
    {
        Vector2 newTwoTouchesMiddlePosition = Vector2.Lerp(Input.GetTouch(0).position, Input.GetTouch(1).position, 0.5f);
        Vector2 twoTouchesMoveVector = oldTwoTouchesMiddlePosition - newTwoTouchesMiddlePosition;
        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
        Vector3 moveVector = cameraYRotation * new Vector3(twoTouchesMoveVector.x, 0, twoTouchesMoveVector.y) * cameraTwoTouchesMovingSpeed;
        moveVector *= (cameraManager.Zoom + 1) * cameraManager.ZoomCameraSpeedModifier;
        oldTwoTouchesMiddlePosition = newTwoTouchesMiddlePosition;
        return moveVector;
    }
    Vector3 GetMoveVectorByTouch()
    {
        Vector2 touchMove = oldTouchPosition - Input.GetTouch(0).position;
        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
        Vector3 moveVector = cameraYRotation * new Vector3(touchMove.x, 0, touchMove.y) * cameraOneTouchMovingSpeed;
        moveVector *= (cameraManager.Zoom + 1) * cameraManager.ZoomCameraSpeedModifier;
        oldTouchPosition = Input.GetTouch(0).position;
        return moveVector;
    }
    float GetRotationAngle()
    {
        Vector2 twoTouchesVector = Input.GetTouch(0).position - Input.GetTouch(1).position;
        float twoTouchesRotationAngle = Vector2.SignedAngle(twoTouchesVector, oldTwoTouchesVector);
        oldTwoTouchesVector = twoTouchesVector;
        return twoTouchesRotationAngle;
    }
    float GetZoomModifier()
    {
        float zoomTouchesDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        float zoomFactor = oldZoomTouchesDistance - zoomTouchesDistance;
        oldZoomTouchesDistance = zoomTouchesDistance;
        return zoomFactor * cameraZoomSpeed;
    }
}
