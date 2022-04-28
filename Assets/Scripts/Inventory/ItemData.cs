using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public enum itemType
    {
        meleeWeapon,
        throwingWeapon,
        bow,
        usableItem,
        nonusableItem
    }
    public enum sizeInInventory
    {
        oneCell,
        twoCells,
        threeCells,
        fourCells,
    }
    public string Name;
    public string Description;
    public int itemID;
    public int cost;
    public itemType type;
    public sizeInInventory size;
    public Sprite icon;
    public GameObject prefab;
}
