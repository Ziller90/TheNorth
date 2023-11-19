using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapTerrain : MonoBehaviour
{

    ClickableObject clickableObject;
    private void Awake()
    {
        clickableObject = GetComponent<ClickableObject>();
    }
    private void OnEnable()
    {
        clickableObject.clickEventWithPosition += MoveToPosition;
    }
    private void OnDisable()
    {
        clickableObject.clickEventWithPosition -= MoveToPosition;
    }
    void MoveToPosition(Vector3 position)
    {
        GlobalMapLinks.instance.playerSquad.MoveToPosition(position);
    }
}
