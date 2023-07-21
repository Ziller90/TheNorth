using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public delegate void UpdateSelectionState(bool state);
    public UpdateSelectionState updateSelectionStateEvent;

    [SerializeField] bool isInteractable = true;

    public void SetInteractable(bool isInteractable)
    {
        if (isInteractable)
        {
            this.isInteractable = true;
            Links.instance.globalLists.AddInteractableOnLocation(this);
        }
        else if (!isInteractable)
        {
            this.isInteractable = false;
            Links.instance.globalLists.RemoveInteractableOnLocation(this);
        }
    }
    public void SetHighlighted(bool highlighted)
    {
        updateSelectionStateEvent(highlighted);
    }
    void OnEnable()
    {
        SetInteractable(isInteractable);
    }
    void OnDisable()
    {
        SetInteractable(isInteractable);
    }
}
