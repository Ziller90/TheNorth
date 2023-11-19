using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public delegate void UpdateSelectionState(bool state);
    public UpdateSelectionState updateSelectionStateEvent;

    [SerializeField] bool isInteractableCached = true;

    public void SetInteractable(bool isInteractable)
    {
        if (isInteractable)
        {
            this.isInteractableCached = true;
            Links.instance.globalLists.AddInteractableOnLocation(this);
        }
        else if (!isInteractable)
        {
            this.isInteractableCached = false;
            Links.instance.globalLists.RemoveInteractableOnLocation(this);
        }
    }
    public void SetHighlighted(bool highlighted)
    {
        updateSelectionStateEvent?.Invoke(highlighted);
    }
    void OnEnable()
    {
        SetInteractable(isInteractableCached);
    }
    void OnDisable()
    {
        SetInteractable(isInteractableCached);
    }
}
