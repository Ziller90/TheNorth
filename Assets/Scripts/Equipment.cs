using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] AnimationEvents animationEvents;
    [SerializeField] Creature creature;
    [SerializeField] Transform rightHandItemKeeper;
    [SerializeField] Transform leftHandItemKeeper;

    [SerializeField] Item rightHandItem;
    [SerializeField] Item leftHandItem;

    [SerializeField] Item[] quickAccessItems = new Item[3];

    public Item RightHandItem => rightHandItem;
    public Item LeftHandItem => leftHandItem;
    public Item[] QuickAccessItems => quickAccessItems;

    public void SetItemInRightHand(Item newItem)
    {
        rightHandItem = newItem;
        newItem.transform.SetParent(rightHandItemKeeper);
        newItem.transform.position = rightHandItemKeeper.position;
        newItem.transform.rotation = rightHandItemKeeper.rotation;

        var meleeWeapon = newItem.GetComponentInChildren<MeleeWeapon>();
        if (meleeWeapon)
        {
            meleeWeapon.SetMeleeWeapon(creature);
            animationEvents.SetMeleeWeapon(meleeWeapon);
        }
    }
    public void SetItemInLeftHand(Item newItem)
    {
        leftHandItem = newItem;
        newItem.transform.SetParent(leftHandItemKeeper);
        newItem.transform.position = leftHandItemKeeper.position;
        newItem.transform.rotation = leftHandItemKeeper.rotation;

        var shield = newItem.GetComponentInChildren<MeleeWeapon>();
    }
    public void SetItemInQuickAccessSlot(Item newItem, int index)
    {
        quickAccessItems[index] = newItem;
    }
    public void RemoveItemFromRightHand()
    {
        rightHandItem.transform.position = new Vector3(-1000, -1000, -1000);
        rightHandItem = null;
    }
    public void RemoveItemFromLeftHand()
    {
        leftHandItem = null;
    }
    public void RemoveItemFromQuickAccessSlot(int index)
    {
        quickAccessItems[index] = null;
    }
}
