using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlService : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float cameraYRotation;
    [SerializeField] Quaternion startRotation;
    [SerializeField] Vector3 startOffset;
    [SerializeField] float zoom;
    [SerializeField] bool follow;

    GameObject objectToFollow;

    public float CameraYRotation => cameraYRotation;
    public Camera MainCamera => mainCamera;

    void FixedUpdate()
    {
        cameraYRotation = mainCamera.transform.rotation.eulerAngles.y;

        if (follow)
            FollowObject();
    }

    void FollowObject()
    {
        mainCamera.transform.SetPositionAndRotation(objectToFollow.transform.position + startOffset * zoom, startRotation);
    }

    public void SetObjectToFollow(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
    }

    public Vector3 WorldToScreenPoint(Vector3 point)
    {
        return mainCamera.WorldToScreenPoint(point);
    }
}


