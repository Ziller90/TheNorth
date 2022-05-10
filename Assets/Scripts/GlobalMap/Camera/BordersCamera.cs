using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersCamera : MonoBehaviour
{
    private Vector3 startPositionCamera;

    [SerializeField] private float minZoomBorder = 1;
    [SerializeField] private float maxZoomBorder = 10;

    [SerializeField] private float widthMap = 100;
    [SerializeField] private float heightMap = 100;

    void Start()
    {
        startPositionCamera = new Vector3(transform.position.x,0,transform.position.z);
    }

    public void MoveBorders()
    {
        if (transform.position.x > (startPositionCamera.x + widthMap / 2))
        {
            transform.position = new Vector3((startPositionCamera.x + widthMap / 2), transform.position.y, transform.position.z);
        }
        if (transform.position.x < (startPositionCamera.x - widthMap / 2))
        {
            transform.position = new Vector3((startPositionCamera.x - widthMap / 2), transform.position.y, transform.position.z);
        }

        if (transform.position.z > (startPositionCamera.z + heightMap / 2))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (startPositionCamera.z + heightMap / 2));
        }
        if (transform.position.z < (startPositionCamera.z - heightMap / 2))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (startPositionCamera.z - heightMap / 2));
        }

    }

    public void ZoomBorders()
    {
        if (transform.position.y < minZoomBorder)
        {
            transform.position = new Vector3(transform.position.x, minZoomBorder, transform.position.z);
        }
        if (transform.position.y > maxZoomBorder)
        {
            transform.position = new Vector3(transform.position.x, maxZoomBorder, transform.position.z);
        }
    }
}
