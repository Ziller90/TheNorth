using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingState
{
    Idle,
    Walk,
    Run
}
public class CharacterContoller : MonoBehaviour
{
    [SerializeField] float runSpeed; // km per hour
    [SerializeField] float walkSpeed; // km per hour
    [SerializeField] float rotationSpeed;
    [SerializeField] Animator humanAnimator;
    [SerializeField] ControlManager controlManager;

    [HideInInspector] public bool allowMoving;
    [HideInInspector] public bool allowRunning;
    [HideInInspector] public bool allowRotation;

    MovingState movingState;

    public void SetControlManager(ControlManager controlManager)
    {
        this.controlManager = controlManager;
    }
    public void MoveForward()
    {
        if (controlManager.GetMovingMode() == MovingMode.Walk)
        {
            Walk();
        }
        else if (controlManager.GetMovingMode() == MovingMode.Run)
        {
            if (allowRunning)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            Idle();
        }
    }
    public void Walk() 
    {
        transform.position += transform.forward * Utils.SpeedConverter(walkSpeed);
        movingState = MovingState.Walk;
    }
    public void Run()
    {
        transform.position += transform.forward * Utils.SpeedConverter(runSpeed);
        movingState = MovingState.Run;
    }
    public void Idle()
    {
        movingState = MovingState.Idle;
    }
    public void Rotate()
    {
        if (controlManager.GetMovingMode() == MovingMode.Walk || controlManager.GetMovingMode() == MovingMode.Run)
        {
            Quaternion lookRotation = Quaternion.LookRotation(controlManager.GetDirection());
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
        }
    }
    public void LookAtPoint(Vector3 point)
    {
        point.y = transform.position.y;
        transform.LookAt(point);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        if (allowMoving == true)
        {
            MoveForward();
        }
        else
        {
            Idle();
        }

        if (allowRotation == true)
        {
            Rotate();
        }

        if (movingState == MovingState.Idle)
        {
            humanAnimator.SetInteger("MoveIndex", 1);
        }
        if (movingState == MovingState.Walk)
        {
            humanAnimator.SetInteger("MoveIndex", 2);
        }
        if (movingState == MovingState.Run)
        {
            humanAnimator.SetInteger("MoveIndex", 3);
        }
    }
}
