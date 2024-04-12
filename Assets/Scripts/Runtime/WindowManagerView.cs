using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowManagerView : MonoBehaviour
{
    [SerializeField] RectTransform windowsContainer;
    [SerializeField] GameObject mobileInterface;

    Dictionary<int, WindowView> windowMap = new();
    int lastWindowId = 1;

    public GameObject ShowWindow(GameObject prefab, MonoBehaviour presentation, bool hideMobileInterface)
    {
        var windowObject = Instantiate(prefab, windowsContainer.transform);

        var newWindowView = windowObject.GetComponent<WindowView>();
        newWindowView.windowHiddenEvent -= HideWindow;
        newWindowView.windowHiddenEvent += HideWindow;
        int newWindowId = lastWindowId + 1;

        newWindowView.SetId(newWindowId);
        newWindowView.HidesMobileInterface = hideMobileInterface;
        mobileInterface.SetActive(!hideMobileInterface);

        try
        {
            newWindowView.SetPresentation(presentation);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

        windowMap.Add(newWindowId, newWindowView);
        lastWindowId = newWindowId;
        Debug.Log("Window " + newWindowId + " opened");

        return windowObject;
    }

    public void HideWindow(int id)
    {
        if (!windowMap.TryGetValue(id, out WindowView windowView))
            return;
        Debug.Log("Window " + id + " closed");
        windowMap.Remove(id);

        if (windowMap.Count == 0 || windowMap.All(i => i.Value.HidesMobileInterface == false))
            mobileInterface.SetActive(true);

        Destroy(windowView.gameObject);
    }
}
