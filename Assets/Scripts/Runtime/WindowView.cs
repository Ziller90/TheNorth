using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    public delegate void WindowHiddenEvent(int id);

    int id;

    [SerializeField]
    bool canBeSkipped = true;

    public bool CanBeSkipped => canBeSkipped;
    public int Id => id;

    public void SetId(int id)
    {
        this.id = id;
    }

    public event WindowHiddenEvent windowHiddenEvent;

    protected void PostWindowHiddenEvent()
    {
        windowHiddenEvent?.Invoke(id);
    }

    public virtual void HideWindow()
    {
        PostWindowHiddenEvent();
    }

    // TODO: Rework method. Make not virtual, instead create `getPresentation` generic method.
    public virtual void SetPresentation(MonoBehaviour presentation)
    {
    }
}
