using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPanelsView : MonoBehaviour
{
    [SerializeField] GameObject hudPanelPrefab;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] float hudPanelYOffset;

    Dictionary<GameObject, HUDPanel> hudPanelsMap = new();

    public void OnUnitRegistered(Transform unit)
    {
        var health = unit.gameObject.GetComponent<Health>();
        if (!health)
            return;

        var hudPanel = Instantiate(hudPanelPrefab, transform).GetComponent<HUDPanel>();
        hudPanel.SetObservedObject(unit.gameObject, this);
        hudPanelsMap.Add(unit.gameObject, hudPanel);
    }

    public void OnUnitUnregistered(Transform unit)
    {
        if (hudPanelsMap.TryGetValue(unit.gameObject, out var hudPanel))
        {
            Destroy(hudPanel.gameObject);
            hudPanelsMap.Remove(unit.gameObject);
        }
    }

    void OnEnable()
    {
        Game.ActorsAccessModel.unitRegisterd -= OnUnitRegistered;
        Game.ActorsAccessModel.unitRegisterd += OnUnitRegistered;
        Game.ActorsAccessModel.unitUnregisterd -= OnUnitUnregistered;
        Game.ActorsAccessModel.unitUnregisterd += OnUnitUnregistered;
    }

    void OnDisable()
    {
        Game.ActorsAccessModel.unitRegisterd -= OnUnitRegistered;
        Game.ActorsAccessModel.unitRegisterd -= OnUnitUnregistered;
    }

    Vector2 CalculateCanvasPosition(HUDPanel hudPanel)
    {
        // Converts screen point (camera coords) to canvas point.
        var screenPoint = hudPanel.Position;
        Vector2 canvasPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            screenPoint,
            null,
            out canvasPoint);
        canvasPoint.y += hudPanelYOffset;
        return canvasPoint;
    }

    void LateUpdate()
    {
        List<GameObject> keysToRemove = new(512);
        foreach (var pair in hudPanelsMap)
        {
            if (pair.Key)
            {
                var hudPanel = pair.Value;
                if (hudPanel.IsPanelVisible())
                {
                    hudPanel.gameObject.SetActive(true);
                    (hudPanel.transform as RectTransform).anchoredPosition = CalculateCanvasPosition(hudPanel);
                }
                else
                {
                    hudPanel.gameObject.SetActive(false);
                }
            }
            else
            {
                keysToRemove.Add(pair.Key);
            }
        }
        foreach (var key in keysToRemove)
        {
            hudPanelsMap.Remove(key);
        }
    }
}
