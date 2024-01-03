using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUsing : MonoBehaviour
{
    [SerializeField] protected bool destroyOnUse;
    public bool DestroyOnUse => destroyOnUse;
    public abstract void UseItem(Unit userUnit);
}
