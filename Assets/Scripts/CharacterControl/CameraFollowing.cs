using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [HideInInspector] public float cameraYRotation;
    [SerializeField] GameObject objectToFollow;
    [SerializeField] Vector3 standartOffset;
    [SerializeField] float zoom;

    void FixedUpdate()
    {
        cameraYRotation = gameObject.transform.rotation.eulerAngles.y;
        gameObject.transform.position = objectToFollow.transform.position + standartOffset * zoom;
    }
}


