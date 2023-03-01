using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RTSCamera : MonoBehaviour
{
    [SerializeField] float cameraMovingSpeed;
    [SerializeField] float cameraZoomSpeed;
    [SerializeField] float cameraRotationSpeed;
    [SerializeField] BoxCollider cameraBordersBox;

    Touch oldTouch;
    Vector2 oldTwoTouchesVector;
    bool movingOnPreviousFrame = false;
    bool zoomingOnPreviousFrame = false;
    float oldZoomTouchesDistance;
    Vector3 cameraObservedPoint;

    void Update()
    {
        DefineObservedPoint();

        if (Input.touchCount == 2)
        {
            if (!zoomingOnPreviousFrame)
            {
                oldZoomTouchesDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                oldTwoTouchesVector = Input.GetTouch(0).position - Input.GetTouch(1).position;
                zoomingOnPreviousFrame = true;
            }
            float zoomTouchesDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float zoomFactor = zoomTouchesDistance - oldZoomTouchesDistance;
            transform.position += transform.forward * zoomFactor * cameraZoomSpeed;
            oldZoomTouchesDistance = zoomTouchesDistance;

            Vector2 twoTouchesVector = Input.GetTouch(0).position - Input.GetTouch(1).position;
            float twoTouchesRotationAngle = Vector2.SignedAngle(twoTouchesVector, oldTwoTouchesVector);
            Vector3 rotationPoint = new Vector3(cameraObservedPoint.x, 0, cameraObservedPoint.z);
            transform.RotateAround(rotationPoint, Vector3.up, twoTouchesRotationAngle * cameraRotationSpeed);
            oldTwoTouchesVector = twoTouchesVector;
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
            Vector2 touchMove = oldTouch.position - Input.GetTouch(0).position;
            Quaternion cameraYRotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
            transform.position += cameraYRotation * new Vector3(touchMove.x, 0, touchMove.y) * cameraMovingSpeed;
            oldTouch = Input.GetTouch(0);
        }
        else
        {
            movingOnPreviousFrame = false;
        }
    }
    private void LateUpdate()
    {
        SetCameraInBox();
    }
    void DefineObservedPoint()
    {
        RaycastHit hit;
        bool hitObject = Physics.Raycast(transform.position, transform.forward, out hit, 100000);
        if (hitObject)
        {
            cameraObservedPoint = hit.point;
        }
    }
    void SetCameraInBox()
    {
        float Xborder = cameraBordersBox.center.x + cameraBordersBox.size.x / 2;
        float Zborder = cameraBordersBox.center.z + cameraBordersBox.size.z / 2;

        float ClampedX = Mathf.Clamp(transform.position.x, -Xborder, Xborder);
        float ClampedZ = Mathf.Clamp(transform.position.z, -Zborder, Zborder);

        transform.position = new Vector3(ClampedX, transform.position.y, ClampedZ);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cameraObservedPoint, 0.5f);
    }
}
