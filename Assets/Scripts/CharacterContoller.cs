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
    public MovingState movingState;
    public float minModificatorToWalk; // Minimal modificator value when character starts walking
    public float modificatorToRun; // Minimal modificator value when character starts runing
    public float runSpeed; // km per hour
    public float walkSpeed; // km per hour
    public float rotationSpeed;
    public bool allowMoving;
    public bool allowRotation;

    public Animator humanAnimator;
    public ControlManager controlManager;
    Transform transform;

    void Start()
    {
        transform = gameObject.transform;
    }
    public void MoveForward()
    {
        if (controlManager.GetSpeedModificator() > minModificatorToWalk && controlManager.GetSpeedModificator() < modificatorToRun)
        {
            transform.position += transform.forward * Utils.SpeedConverter(walkSpeed);
            movingState = MovingState.Walk;
        }
        else if (controlManager.GetSpeedModificator() > modificatorToRun)
        {
            transform.position += transform.forward * Utils.SpeedConverter(runSpeed);
            movingState = MovingState.Run;
        }
        else
        {
            movingState = MovingState.Idle;
        }
    }
    public void Rotate()
    {
        if (controlManager.GetSpeedModificator() > minModificatorToWalk)
        {
            Quaternion LookRotation = Quaternion.LookRotation(controlManager.GetDirection());
            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookRotation, rotationSpeed);
        }
    }

    void FixedUpdate()
    {
        if (allowMoving == true)
        {
            MoveForward();
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
