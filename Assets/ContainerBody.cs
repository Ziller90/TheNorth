using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContainerBody : MonoBehaviour
{
    public UnityEvent containerOpened;
    public UnityEvent containerClosed;
    public void OpenContainer()
    {
        containerOpened?.Invoke();
    }
    public void CloseContainer()
    {
        containerClosed?.Invoke();
    }
}
