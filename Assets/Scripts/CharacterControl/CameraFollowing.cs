using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] float cameraYRotation;
    [SerializeField] Quaternion startRotation;
    [SerializeField] Vector3 startOffset;
    [SerializeField] GameObject objectToFollow;
    [SerializeField] float zoom;
    [SerializeField] bool follow;
    public float CameraYRotation => cameraYRotation;

    void FixedUpdate()
    {
        cameraYRotation = gameObject.transform.rotation.eulerAngles.y;
        if (follow)
        {
            gameObject.transform.position = objectToFollow.transform.position + startOffset * zoom;
            gameObject.transform.rotation = startRotation;
        }
    }
    public void SetObjectToFollow(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
    }
}


