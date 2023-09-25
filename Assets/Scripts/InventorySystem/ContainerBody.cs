using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ContainerBody : MonoBehaviour
{
    public Action containerOpenedEvent;
    public Action containerClosedEvent;

    public void OpenContainer()
    {
        containerOpenedEvent?.Invoke();
    }

    public void CloseContainer()
    {
        containerClosedEvent?.Invoke();
    }
}
