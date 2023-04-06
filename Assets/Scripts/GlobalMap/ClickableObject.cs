using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class ClickableObject : MonoBehaviour
{
    public delegate void OnClickDelegate(Vector3 clickPosition);
    public event OnClickDelegate OnClickOnObjectWithPosition;
    public event Action OnClickOnObject;
    public void ClickOnObject(Vector3 clickPosition)
    {
        OnClickOnObjectWithPosition?.Invoke(clickPosition);
    }
    public void ClickOnObject()
    {
        OnClickOnObject?.Invoke();
    }
}


