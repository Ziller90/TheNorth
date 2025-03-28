using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum MovingState
{
    Idle,
    Walk,
    Run
}

public class CharacterContoller : MonoBehaviourPun
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
        else if (controlManager.MovingMode == MovingMode.Run)
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
        if (!ScenesLauncher.isMultiplayer|| (ScenesLauncher.isMultiplayer && GetComponent<PhotonView>().IsMine))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            if (AllowMoving)
            {
                MoveForward();
            }
            else
            {
                Idle();
            }

            if (AllowRotation)
            {
                Rotate();
            }

            int moveIndex = 1; // Idle
            if (movingState == MovingState.Walk)
            {
                moveIndex = 2;
            }
            else if (movingState == MovingState.Run)
            {
                moveIndex = 3;
            }
            SyncOrSetMoveIndex(moveIndex);
        }
    }

    private void SyncOrSetMoveIndex(int index)
    {
        if (ScenesLauncher.isMultiplayer)
        {
            photonView.RPC("RPC_SetMoveIndex", RpcTarget.All, index);
        }
        else
        {
            animator.SetInteger("MoveIndex", index);
        }
    }

    [PunRPC]
    private void RPC_SetMoveIndex(int index)
    {
        animator.SetInteger("MoveIndex", index);
    }
}
