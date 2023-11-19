using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapPlayerController : MonoBehaviour
{
    GlobalMapSquad squadController;
    ControlManager controlManager;
    void Start()
    {
        squadController = GetComponent<GlobalMapSquad>();   
    }
    public void SetNewDestinationPoint(Vector3 screenClickPosition)
    {
        squadController.MoveToPosition(screenClickPosition);
    }
}
