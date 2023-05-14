using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] Item rightHandItem;
    [SerializeField] Item leftHandItem;

    [SerializeField] Item[] quickAccessItems;

    public Item RightHandItem => rightHandItem;
    public Item LeftHandItem => leftHandItem;
    public Item[] QuickAccessItems => quickAccessItems;

}
