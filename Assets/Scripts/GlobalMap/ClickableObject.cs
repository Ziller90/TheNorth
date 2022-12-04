using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ClickableObject : MonoBehaviour
{
    public UnityEvent OnClick;
    public void ClickOnObject()
    {
        OnClick.Invoke();
    }
}


