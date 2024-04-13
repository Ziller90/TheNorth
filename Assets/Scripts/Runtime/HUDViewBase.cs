using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUDViewBase : MonoBehaviour
{
    public abstract void SetObservedObject(GameObject observedObject, HUDPanelsView hudPanelsView = null);
    public abstract bool IsVisible();
}
