using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [HideInInspector] public float cameraYRotation;
    [SerializeField] Quaternion startRotation;
    [SerializeField] Vector3 startOffset;

    [SerializeField] GameObject objectToFollow;
    [SerializeField] Vector3 currentOffset;
    [SerializeField] Quaternion currentRotation;
    [SerializeField] float zoom;
    [SerializeField] bool follow;

    private void Awake()
    {
        Debug.Log("Awake");
        currentOffset = startOffset;
        currentRotation = startRotation;
    }
    void FixedUpdate()
    {
        cameraYRotation = gameObject.transform.rotation.eulerAngles.y;
        if (follow)
        {
            gameObject.transform.position = objectToFollow.transform.position + currentOffset * zoom;
            gameObject.transform.rotation = currentRotation;
        }
    }
    public void SetObjectToFollow(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
    }
    //private void OnValidate()
    //{
    //    currentOffset = gameObject.transform.position - objectToFollow.transform.position;
    //    currentRotation = gameObject.transform.rotation;
    //}
    [ContextMenu("SetCurrentOffsetAndRotation")]
    public void SetCurrentOffsetAndRotation()
    {
        currentOffset = gameObject.transform.position - objectToFollow.transform.position;
        currentRotation = gameObject.transform.rotation;
    }
}


