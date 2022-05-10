using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPointToMove : MonoBehaviour, IPointerClickHandler
{
    private float timerClick = 10;
    [SerializeField] private float speedTimerClick = 1;
    private MovePlayerToPoint movePlayerToPoint;
    [SerializeField] private GameObject Player;
    void Awake()
    {
        movePlayerToPoint = Player.GetComponent<MovePlayerToPoint>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (timerClick > 0)
        {
            movePlayerToPoint.newPointToMove(eventData.pointerCurrentRaycast.worldPosition);
        }
        timerClick = 10;
    }
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (timerClick > 0)
            {
                timerClick = timerClick - speedTimerClick * Time.deltaTime;
            }
        }
    }
}