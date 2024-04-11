using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraService : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] float cameraYRotation;
    [SerializeField] Quaternion startRotation;
    [SerializeField] Vector3 startOffset;
    [SerializeField] GameObject objectToFollow;
    [SerializeField] float zoom;
    [SerializeField] bool follow;

    public float CameraYRotation => cameraYRotation;
    public GameObject MainCamera => mainCamera;

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
}


