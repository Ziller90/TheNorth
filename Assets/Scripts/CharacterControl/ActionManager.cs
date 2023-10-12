 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour
{
    public UnityAction OnOpenInventoryPressed;
    public UnityAction OnInteractPressed;

    public bool mainWeaponUsing;
    public bool secondaryWeaponUsing;
}
