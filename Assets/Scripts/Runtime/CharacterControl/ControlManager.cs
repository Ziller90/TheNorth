using UnityEngine;

public enum MovingMode
{
    Run, 
    Walk,
    Stand
}

public class ControlManager : MonoBehaviour
{
    Vector3 direction = new Vector3(0,0,0);
    MovingMode movingMode = MovingMode.Stand;

    public Vector3 Direction => direction;
    public MovingMode MovingMode => movingMode; 

    public void SetControl(Vector3 direction, MovingMode movingMode)
    {
        this.direction = direction;
        this.movingMode = movingMode;
    }
}
