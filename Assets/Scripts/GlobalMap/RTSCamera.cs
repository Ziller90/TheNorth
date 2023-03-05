using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class RTSCamera : MonoBehaviour
{
    [SerializeField] BoxCollider cameraBordersBox;
    [SerializeField] GameObject startObservedObject;

    [SerializeField] float zoom;
    [SerializeField] float rotationAngle;
    [SerializeField] float viewAngle;
    [SerializeField] Vector3 observedPoint;
    [SerializeField] float cameraHeight;

    [SerializeField] float minDistanceToObservedPoint;
    [SerializeField] float maxDistanceToObservedPoint;
    [SerializeField] float zoomCameraSpeedModifier;
    public float Zoom => zoom;
    public float RotationAngle => rotationAngle;
    public Vector3 ObservedPoint => observedPoint;

    Vector3 rotationPoint;
    Vector3 realObservingPoint;

    public void Awake()
    {
        SetObservedPoint(new Vector3(startObservedObject.transform.position.x, 0, startObservedObject.transform.position.z));
    }
    public void SetViewAngle(float viewAngle)
    {
        transform.rotation = Quaternion.Euler(viewAngle, transform.rotation.y, transform.rotation.z);
        this.viewAngle = viewAngle;
        SetObservedPoint(observedPoint);
    }
    public void SetObservedPoint(Vector3 newObservedPoint)
    {
        observedPoint = SetPointInBox(newObservedPoint);
        transform.position = observedPoint + Quaternion.Euler(-(90 - viewAngle), 0, 0) * Vector3.up;
        transform.rotation = Quaternion.Euler(viewAngle, 0, transform.rotation.z);
        SetRotation(rotationAngle);
        SetZoom(zoom);
    }
    Vector3 GetRealObservedPoint(Vector3 observedPoint)
    {
        RaycastHit hit;
        bool hitObject = Physics.Raycast(transform.position, observedPoint - transform.position, out hit, 100000);
        if (hitObject)
        {
            return hit.point;
        }
        return observedPoint;
    }
    public void SetZoom(float zoom)
    {
        zoom = Mathf.Clamp01(zoom);
        this.zoom = zoom;
        Vector3 observedPointToCamera = (transform.position - observedPoint).normalized;
        Vector3 minZoomBoundingPoint = observedPoint + observedPointToCamera * minDistanceToObservedPoint;
        Vector3 maxZoomBoundingPoint = observedPoint + observedPointToCamera * maxDistanceToObservedPoint;
        transform.position = Vector3.Lerp(minZoomBoundingPoint, maxZoomBoundingPoint, zoom);
    }
    public void SetRotation(float angle)
    {
        rotationPoint = GetRotatioinPoint(observedPoint);
        float currentRotationAngle = GetCurrentRotationAngle();
        float difference = angle - currentRotationAngle;
        transform.RotateAround(rotationPoint, Vector3.up, -difference);
        rotationAngle = GetCurrentRotationAngle();
    }
    Vector3 GetRotatioinPoint(Vector3 ObservedPoint)
    {
        return new Vector3(observedPoint.x, transform.position.y, observedPoint.z);
    }
    public float GetCurrentRotationAngle()
    {
        return Vector3.SignedAngle(transform.position - rotationPoint, new Vector3(0,0,-1), Vector3.up);
    }
    Vector3 SetPointInBox(Vector3 point)
    {
        float Xborder = cameraBordersBox.center.x + cameraBordersBox.size.x / 2;
        float Zborder = cameraBordersBox.center.z + cameraBordersBox.size.z / 2;

        float ClampedX = Mathf.Clamp(point.x, -Xborder, Xborder);
        float ClampedZ = Mathf.Clamp(point.z, -Zborder, Zborder);

        return new Vector3(ClampedX, point.y, ClampedZ);
    }
    private void OnValidate()
    {
        SetViewAngle(viewAngle);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(observedPoint, 0.5f);
        Gizmos.DrawRay(transform.position, observedPoint - transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rotationPoint, 0.5f);
        Gizmos.DrawWireSphere(rotationPoint, Vector3.Distance(rotationPoint, transform.position));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rotationPoint, new Vector3(0, 0, -1));
        Gizmos.DrawRay(observedPoint, Quaternion.Euler(-(90 - viewAngle), 0, 0) * Vector3.up * 10);
    }
}
