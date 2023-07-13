using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public delegate void UpdateSelectionState(bool state);
    public UpdateSelectionState updateSelectionStateEvent;
    public bool IsInteractable { get; set; } = true;
    public void SetHighlighted(bool highlighted)
    {
        updateSelectionStateEvent(highlighted);
    }
    void OnEnable()
    {
        if (IsInteractable)
            Links.instance.globalLists.AddInteractableOnLocation(this);
    }
    void OnDisable()
    {
        if (IsInteractable)
            Links.instance.globalLists.RemoveInteractableOnLocation(this);
    }
}
