using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUsing : MonoBehaviour
{
    [SerializeField] protected bool destroyOnUse;
    public abstract void UseItem(Creature userCreature);
}
