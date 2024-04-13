using UnityEngine;

public class FactionMarker : MonoBehaviour
{
    [SerializeField] FactionsConfig.FactionId faction;

    public FactionsConfig.FactionId Faction => faction;
}
