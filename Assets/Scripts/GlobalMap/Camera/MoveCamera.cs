using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{
    private BordersCamera bordersCamera;
    private Vector2? startZoomPosition1Touch;
    private Vector2? startZoomPosition2Touch;

    private Vector3 startPositionCamera;
    private Vector3 startPositionMoveCamera;

    private Vector2 startPositionMoveTouch;

    [SerializeField] private float moveSensitivity = 0.01f;
    [SerializeField] private float zoomSensitivity = 0.1f;

    void Awake()
    {
        bordersCamera = GetComponent<BordersCamera>();
    }

    void Update()
    { 
        if (Input.touchCount == 2)
        {
            ZoomScale();
            startPositionMoveTouch = Vector2.zero;
            startPositionMoveCamera = transform.position;
        }
        else
        {
            if (Input.touchCount == 1)
            {
                Move();
            }
            else
            {
                startPositionMoveTouch = Vector2.zero;
                startPositionMoveCamera = transform.position;
            }
            ResetZoomStartPosotoin();
            startPositionCamera = transform.position;
        }
  
    }
    private void Move()
    {
        if (startPositionMoveTouch == Vector2.zero)
        {
            startPositionMoveTouch = Input.GetTouch(0).position;
        }
        Vector2 positionMoveTouch = Input.GetTouch(0).position;
        Vector2 alphaPosition = startPositionMoveTouch - positionMoveTouch;
        transform.position = new Vector3(startPositionMoveCamera.x+ alphaPosition.x * moveSensitivity, transform.position.y, startPositionMoveCamera.z + alphaPosition.y * moveSensitivity);
        bordersCamera.MoveBorders();
    }

    void ResetZoomStartPosotoin()
    {
        startZoomPosition1Touch = null;
        startZoomPosition2Touch = null;
    }

    void ZoomScale()
    {
        if (!startZoomPosition1Touch.HasValue && !startZoomPosition2Touch.HasValue)
        {
            startZoomPosition1Touch = Input.GetTouch(0).position;
            startZoomPosition2Touch = Input.GetTouch(1).position;
        }
        Vector2 touchPosition1 = Input.GetTouch(0).position;
        Vector2 touchPosition2 = Input.GetTouch(1).position;
        transform.position = new Vector3(transform.position.x, startPositionCamera.y + (Vector2.Distance(startZoomPosition2Touch.Value, startZoomPosition1Touch.Value) - Vector2.Distance(touchPosition2, touchPosition1)) * zoomSensitivity, transform.position.z);
        bordersCamera.ZoomBorders();
    }
}
