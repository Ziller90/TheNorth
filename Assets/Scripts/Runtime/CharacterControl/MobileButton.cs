using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPressed;
    public UnityEvent OnReleased;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnPressed?.Invoke();
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        OnReleased?.Invoke();
    }
}
