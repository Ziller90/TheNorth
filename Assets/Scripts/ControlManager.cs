using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    Vector3 direction;
    float speedModificator; 

    public void SetControl(Vector3 direction, float speedModificator)
    {
        this.direction = direction;
        this.speedModificator = speedModificator;
    }
    public Vector3 GetDirection()
    {
        return direction;
    }
    public float GetSpeedModificator()
    {
        return speedModificator;
    }
    private void Start()
    {
        Debug.Log(Utils.SpeedConverter(5));
    }
}
