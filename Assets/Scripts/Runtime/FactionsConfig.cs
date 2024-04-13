using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TheNorth/FactionsConfig")]
public class FactionsConfig : ScriptableObject
{
    [System.Serializable]
    public class Faction
    {
        public FactionId factionId;
        public Color color;
        public Color unitColor;
    }

    public enum FactionId
    {
        Undefined = 0,
        Villagers = 1,
        Bandits = 2,
        Animals = 3,
    }

    [SerializeField]
    List<Faction> factions;

    public IReadOnlyList<Faction> Factions => factions;

    public Faction GetFaction(FactionId factionId)
    {
        return factions.Find(item => item.factionId == factionId);
    }
}
