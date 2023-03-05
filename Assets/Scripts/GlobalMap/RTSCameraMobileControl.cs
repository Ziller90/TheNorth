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

    Touch oldTouch;
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
            cameraManager.SetZoom(cameraManager.Zoom + GetZoomModifier());
            cameraManager.SetRotation(cameraManager.RotationAngle + GetRotationAngle());
        }
        else
        {
            zoomingOnPreviousFrame = false;
        }
        if (Input.touchCount == 1)
        {
            if (!movingOnPreviousFrame)
            {
                oldTouch = Input.GetTouch(0);
                movingOnPreviousFrame = true;
            }
            cameraManager.SetObservedPoint(cameraManager.ObservedPoint + GetMoveVectorByTouches());
        }
        else
        {
            movingOnPreviousFrame = false;
        }
    }
    Vector3 GetMoveVectorByTouches()
    {
        Vector2 touchMove = oldTouch.position - Input.GetTouch(0).position;
        Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
        Vector3 moveVector = cameraYRotation * new Vector3(touchMove.x, 0, touchMove.y) * cameraMovingSpeed;
        oldTouch = Input.GetTouch(0);
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
