using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingUsing : ItemUsing
{
    [SerializeField] float healthBonus;
    public override void UseItem(Unit userUnit)
    {
        userUnit.GetComponent<Health>().Heal(healthBonus);
    }
}
