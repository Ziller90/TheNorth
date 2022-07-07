using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public enum itemType
    {
        MeleeWeapon,
        ThrowingWeapon,
        Bow,
        UsableItem,
        NonusableItem
    }
    public enum sizeInInventory
    {
        OneCell,
        TwoCells,
        ThreeCells,
        FourCells,
    }
    public string name;
    public string description;
    public int itemID;
    public int cost;
    public itemType type;
    public sizeInInventory size;
    public Sprite icon;
    public GameObject prefab;
}
