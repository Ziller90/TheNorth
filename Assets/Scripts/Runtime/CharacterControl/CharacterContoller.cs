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
    
    public bool AllowMoving { get; set; } = true;
    public bool AllowRunning { get; set; } = true;
    public bool AllowRotation { get; set; } = true;

    public MovingState CharacterMovingState => movingState;

    MovingState movingState;
    ControlManager controlManager;
    Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        controlManager = GetComponent<ControlManager>();
    }

    public void MoveForward()
    {
        if (controlManager.MovingMode == MovingMode.Walk)
        {
            Walk();
        }
        else if (controlManager.MovingMode== MovingMode.Run)
        {
            if (AllowRunning)
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
        transform.position += transform.forward * ModelUtils.SpeedConverter(walkSpeed);
        movingState = MovingState.Walk;
    }
    public void Run()
    {
        transform.position += transform.forward * ModelUtils.SpeedConverter(runSpeed);
        movingState = MovingState.Run;
    }
    public void Idle()
    {
        movingState = MovingState.Idle;
    }

    public void Rotate()
    {
        if (controlManager.MovingMode == MovingMode.Walk || controlManager.MovingMode == MovingMode.Run)
        {
            Quaternion lookRotation = Quaternion.LookRotation(controlManager.Direction);
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
        if (AllowMoving == true)
        {
            MoveForward();
        }
        else
        {
            Idle();
        }

        if (AllowRotation == true)
        {
            Rotate();
        }

        if (movingState == MovingState.Idle)
        {
            animator.SetInteger("MoveIndex", 1);
        }
        if (movingState == MovingState.Walk)
        {
            animator.SetInteger("MoveIndex", 2);
        }
        if (movingState == MovingState.Run)
        {
            animator.SetInteger("MoveIndex", 3);
        }
    }
}
