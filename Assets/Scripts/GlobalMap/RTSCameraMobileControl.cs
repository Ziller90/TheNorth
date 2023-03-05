using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSCamera))]
public class RTSCameraMobileControl : MonoBehaviour
{
    [SerializeField] float cameraMovingSpeed;
    [SerializeField] float cameraZoomSpeed;
    [SerializeField] float cameraRotationSpeed;

    Vector2 oldTouchPosition;
    Vector2 oldTwoTouchesVector;
    bool movingOnPreviousFrame = false;
    bool zoomingOnPreviousFrame = false;
    float oldZoomTouchesDistance;
    RTSCamera cameraManager;

    void Start()
    {
        cameraManager = gameObject.GetComponent<RTSCamera>();   
    }
    void Update()
    {
        if (Input.touchCount == 2)
        {
            if (!zoomingOnPreviousFrame)
            {
                oldZoomTouchesDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                oldTwoTouchesVector = Input.GetTouch(0).position - Input.GetTouch(1).position;
                zoomingOnPreviousFrame = true;
            }
            cameraManager.SetRotation(cameraManager.RotationAngle + GetRotationAngle());
            cameraManager.SetZoom(cameraManager.Zoom + GetZoomModifier());
        }
        else
        {
            zoomingOnPreviousFrame = false;
        }
        if (Input.touchCount == 1)
        {
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
    Vector3 GetMoveVectorByTouch()
    {
        Vector2 touchMove = oldTouchPosition - Input.GetTouch(0).position;
        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
        Vector3 moveVector = cameraYRotation * new Vector3(touchMove.x, 0, touchMove.y) * cameraMovingSpeed;
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
