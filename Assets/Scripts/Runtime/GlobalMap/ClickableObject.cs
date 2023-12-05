using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickableObject : MonoBehaviour
{
    public delegate void Click(Vector3 clickPosition);
    public event Click clickEventWithPosition;
    public event Action clickEvent;
    public void ClickOnObject(Vector3 clickPosition)
    {
        clickEventWithPosition?.Invoke(clickPosition);
        clickEvent?.Invoke();
    }
}
