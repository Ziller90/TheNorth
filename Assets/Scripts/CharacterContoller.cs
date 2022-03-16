using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller : MonoBehaviour
{
    public float maxSpeed; // km per hour
    
    public ControlManager controlManager;

    Transform transform;

    void Start()
    {
        transform = gameObject.transform;
    }
    public void MoveForward()
    {
        transform.position += transform.forward * Utils.SpeedConverter(maxSpeed) * controlManager.GetSpeedModificator();
    }
    public void Rotate()
    {
        transform.LookAt(transform.position + controlManager.GetDirection());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveForward();
        Rotate();
    }
}
