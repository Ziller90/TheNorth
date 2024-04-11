using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationEdgeDetector : MonoBehaviour
{
    bool characterInFog = false;
    CharacterContoller characterContoller;

    private void Start()
    {
        characterContoller = GetComponent<CharacterContoller>();    
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.gameObject.GetComponent<LocationEdgeFog>() && !characterInFog)
        {
            characterInFog = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.gameObject.GetComponent<LocationEdgeFog>() && characterInFog)
        {
            characterInFog = false;
        }
    }

    private void Update()
    {
        if (characterInFog && characterContoller.CharacterMovingState == MovingState.Idle)
        {
            Game.GameSceneInitializer.LeaveLocation();
        }
    }
}
