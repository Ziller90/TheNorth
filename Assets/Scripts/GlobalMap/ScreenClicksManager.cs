using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClicksManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public void ScreenClick(Vector2 clickPosition)
    {
        var clickedObject = GetClickedObject(clickPosition);
        if (clickedObject != null && clickedObject.GetComponent<ClickableObject>() != null)
        {
            var clickedPosition = GetClickPositionInWorld(clickPosition);
            clickedObject.GetComponent<ClickableObject>().ClickOnObject(clickedPosition);
        }
    }
    public GameObject GetClickedObject(Vector2 screenClickPosition)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(screenClickPosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public Vector3 GetClickPositionInWorld(Vector2 screenClickPosition)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(screenClickPosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
