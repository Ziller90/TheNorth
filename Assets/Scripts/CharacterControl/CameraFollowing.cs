using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public float cameraYRotation;
    public GameObject objectToFollow;
    public Vector3 standartOffset;
    public float zoom;

    void Start()
    {

    }

    void Update()
    {
        cameraYRotation = gameObject.transform.rotation.eulerAngles.y;
        gameObject.transform.position = objectToFollow.transform.position + standartOffset * zoom;
    }
}
