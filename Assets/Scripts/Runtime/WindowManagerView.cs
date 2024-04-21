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

    public GameObject ShowWindow(GameObject prefab, MonoBehaviour presentation, bool hideMobileInterface, bool hidePreviousWindow)
    {
        var windowObject = Instantiate(prefab, windowsContainer.transform);

        var newWindowView = windowObject.GetComponent<WindowView>();
        newWindowView.windowHiddenEvent -= HideWindow;
        newWindowView.windowHiddenEvent += HideWindow;
        int newWindowId = lastWindowId + 1;

        newWindowView.SetId(newWindowId);
        newWindowView.HidesMobileInterface = hideMobileInterface;
        newWindowView.HidePreviouseWindow = hidePreviousWindow;

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

        if (windowMap.ContainsKey(lastWindowId - 1))
            windowMap[lastWindowId - 1].gameObject.SetActive(!hidePreviousWindow);

        mobileInterface.SetActive(!hideMobileInterface);

        return windowObject;
    }

    public void HideWindow(int id)
    {
        windowMap.TryGetValue(id, out WindowView windowView);

        if (!windowView)
            return;

        if (windowView.HidePreviouseWindow && windowMap.ContainsKey(id - 1))
            windowMap[id - 1].gameObject.SetActive(true);

        windowMap.Remove(id);
        if (id == lastWindowId)
            lastWindowId = id - 1;

        if (windowMap.Count == 0 || windowMap.All(i => i.Value.HidesMobileInterface == false)) 
            mobileInterface.SetActive(true);

        Destroy(windowView.gameObject);
    }
}
