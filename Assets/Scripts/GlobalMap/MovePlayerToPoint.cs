using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToPoint : MonoBehaviour
{
    private Vector3 pointToMove;
    [SerializeField] private float speed = 0.1f;
    void Start()
    {
        pointToMove = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void newPointToMove(Vector3 point)
    {
        pointToMove = new Vector3(point.x, transform.position.y, point.z);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed);
    }
}
