using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonsManager buttonsManager;
    public bool isPressed;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isPressed = false;
    }
}
