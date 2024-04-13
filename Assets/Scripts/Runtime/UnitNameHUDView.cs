using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitNameHUDView : HUDViewBase
{
    [SerializeField] TMP_Text unitNameText;

    FactionMarker factionMarker;
    Unit unit;

    public override bool IsVisible()
    {
        return unit != null && factionMarker != null;
    }

    public override void SetObservedObject(GameObject observedObject, HUDPanelsView hudPanelsView = null)
    {
        unit = observedObject.GetComponent<Unit>();
        factionMarker = observedObject.GetComponent<FactionMarker>();
        unitNameText.text = unit.Name;
        unitNameText.color = Game.FactionsConfig.GetFaction(factionMarker.Faction).unitColor;
    }

    public void Update()
    {
        if (unitNameText.color != Game.FactionsConfig.GetFaction(factionMarker.Faction).unitColor)
            unitNameText.color = Game.FactionsConfig.GetFaction(factionMarker.Faction).unitColor;
    }
}
