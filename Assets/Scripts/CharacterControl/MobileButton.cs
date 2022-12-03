using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    public UnityEvent OnPressed;
    public UnityEvent OnReleased;
    public UnityEvent OnHold;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnPressed?.Invoke();
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        OnReleased?.Invoke();
        isPressed = false;
    }
    private void Update()
    {
        if (isPressed == true)
        {
            OnHold?.Invoke();
        }
    }
}
