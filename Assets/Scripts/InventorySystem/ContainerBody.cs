using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ContainerBody : MonoBehaviour
{
    public Action containerOpened;
    public Action containerClosed;

    public void OpenContainer()
    {
        containerOpened?.Invoke();
    }

    public void CloseContainer()
    {
        containerClosed?.Invoke();
    }
}
