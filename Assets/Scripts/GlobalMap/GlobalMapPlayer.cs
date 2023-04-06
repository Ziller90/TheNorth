using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapPlayer : MonoBehaviour
{
    [SerializeField] ScreenClicksManager screenClicksManager;
    GlobalMapSquad squadController;
    void Start()
    {
        squadController = GetComponent<GlobalMapSquad>();   
    }
    private void OnEnable()
    {
        screenClicksManager.screenClickEvent += SetNewDestinationPoint;
    }
    private void OnDisable()
    {
        screenClicksManager.screenClickEvent -= SetNewDestinationPoint;
    }
    void SetNewDestinationPoint(Vector2 screenClickPosition)
    {
        GameObject clickedObject = screenClicksManager.GetClickedObject(screenClickPosition);
        if (clickedObject.tag == "GlobalMapTerrainPart")
        {
            Vector3 destination = screenClicksManager.GetClickPositionInWorld(screenClickPosition);
            squadController.MoveToPosition(destination);
        }
    }
}
