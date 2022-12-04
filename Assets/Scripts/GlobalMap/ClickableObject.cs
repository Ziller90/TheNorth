using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ClickableObject : MonoBehaviour
{
    [SerializeField] UltEvents.UltEvent OnClickEvent;
    public void ClickOnObject()
    {
        OnClickEvent?.Invoke();
    }
}


