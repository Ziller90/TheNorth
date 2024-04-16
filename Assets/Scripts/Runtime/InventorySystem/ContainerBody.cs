using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.ComponentModel;

public class ContainerBody : MonoBehaviour
{
    public Action containerOpenedEvent;
    public Action containerClosedEvent;

    [SerializeField] bool isDisposableContainer;
    [SerializeField] bool destroyOnDispose;
    [SerializeField] ContainerBase container;
    [SerializeField] MonoBehaviour[] componentsToDeleteOnDispose;

    public void SetComponentsToDeleteOnDispose(params MonoBehaviour[] componentsToDeleteOnDispose)
    {
        this.componentsToDeleteOnDispose = componentsToDeleteOnDispose;
    }
    public void SetDestroyOnDispose(bool destroyOnDispose) => this.destroyOnDispose = destroyOnDispose;
    public void SetIsDisposable(bool isDisposableContainer) => this.isDisposableContainer = isDisposableContainer;
    public void SetContainer(ContainerBase container) => this.container = container;
    public void OpenContainer() => containerOpenedEvent?.Invoke();

    public void CloseContainer()
    {
        if (isDisposableContainer && container.IsEmpty())
        {
            if (destroyOnDispose)
                Destroy(gameObject);
            else
            {
                foreach (var component in componentsToDeleteOnDispose)
                    Destroy(component);
            }
        }
        containerClosedEvent?.Invoke();
    }
}
