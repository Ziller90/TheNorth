using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    int oldTouchCount;
    void CastRay(Vector3 screenPointToRay)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(screenPointToRay);
        if (Physics.Raycast(ray, out hit))
        {
            ClickableObject clickedGameObject = hit.collider.gameObject.GetComponent<ClickableObject>();
            if (clickedGameObject != null)
            {
                clickedGameObject.ClickOnObject();
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && oldTouchCount == 0))
        {
            CastRay(Input.mousePosition);
        }
        oldTouchCount = Input.touchCount;
    }
}
