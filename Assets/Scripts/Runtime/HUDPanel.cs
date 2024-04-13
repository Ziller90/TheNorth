using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPanel : MonoBehaviour
{
    GameObject observedObject;

    public Vector2 Position => Game.CameraControlService.WorldToScreenPoint(observedObject.transform.position);

    public void SetObservedObject(GameObject observedObject, HUDPanelsView hudPanelsView)
    {
        this.observedObject = observedObject;

        foreach (var component in gameObject.GetComponents<HUDViewBase>())
        {
            component.SetObservedObject(observedObject, hudPanelsView);
        }
    }

    public bool IsPanelVisible()
    {
        bool hasVisibleHUDComponent = false;
        foreach (var component in gameObject.GetComponents<HUDViewBase>())
        {
            if (component.IsVisible())
                hasVisibleHUDComponent = true;
        }
        return hasVisibleHUDComponent;
    }
}
