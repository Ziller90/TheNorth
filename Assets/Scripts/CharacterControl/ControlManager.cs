using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingMode
{
    Run, 
    Walk,
    Stand
}
public class ControlManager : MonoBehaviour
{
    Vector3 direction;
    MovingMode movingMode;

    public void SetControl(Vector3 direction, MovingMode movingMode)
    {
        this.direction = direction;
        this.movingMode = movingMode;
    }
    public Vector3 GetDirection()
    {
        return direction;
    } 
    public MovingMode GetMovingMode()
    {
        return movingMode;
    }
}
