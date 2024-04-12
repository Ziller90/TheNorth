using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManagerView : MonoBehaviour
{
    [SerializeField] RectTransform windowsContainer;

    Dictionary<int, WindowView> windowMap = new();
    int lastWindowIndex = 1;

    public GameObject ShowWindow(GameObject prefab, MonoBehaviour presentation)
    {
        var windowObject = Instantiate(prefab, windowsContainer.transform);

        var newWindowView = windowObject.GetComponent<WindowView>();
        newWindowView.windowHiddenEvent -= HideWindow;
        newWindowView.windowHiddenEvent += HideWindow;
        int newWindowIndex = lastWindowIndex + 1;
        newWindowView.SetId(newWindowIndex);
        try
        {
            newWindowView.SetPresentation(presentation);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

        windowMap.Add(newWindowIndex, newWindowView);
        lastWindowIndex = newWindowIndex;
        Debug.Log("Window " + newWindowIndex + " opened");
        return windowObject;
    }

    public void HideLastWindow()
    {
        if (windowMap.Count > 0)
        {
            int maxWindowId = 0;
            foreach (var pair in windowMap)
                if (pair.Key > maxWindowId)
                    maxWindowId = pair.Key;
            if (windowMap[maxWindowId].CanBeSkipped)
                HideWindow(maxWindowId);
        }
    }

    public void HideWindow(int id)
    {
        if (!windowMap.TryGetValue(id, out WindowView windowView))
            return;
        Debug.Log("Window " + id + " closed");
        windowMap.Remove(id);
        Destroy(windowView);
    }
}
