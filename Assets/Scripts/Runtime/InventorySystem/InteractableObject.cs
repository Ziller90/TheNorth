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
            isInteractableCached = true;
            Game.ActorsAccessModel.RegisterInteractableObject(this);
        }
        else if (!isInteractable)
        {
            isInteractableCached = false;
            Game.ActorsAccessModel.RemoveInteractableOnLocation(this);
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

    void OnDestroy()
    {
        Game.ActorsAccessModel.RemoveInteractableOnLocation(this);
    }
}
