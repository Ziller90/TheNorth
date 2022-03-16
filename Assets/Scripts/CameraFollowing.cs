using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public float cameraYRotation;
    public Transform objectToFollow;
    public Vector3 standartOffset;
    public float zoom;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraYRotation = gameObject.transform.rotation.eulerAngles.y;
        gameObject.transform.position = objectToFollow.position + standartOffset * zoom;
    }
}
