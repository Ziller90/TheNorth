using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingUsing : ItemUsing
{
    [SerializeField] float healthBonus;
    public override void UseItem(Creature userCreature)
    {
        userCreature.GetComponent<Health>().Heal(healthBonus);
        if (destroyOnUse)
            Destroy(gameObject);
    }
}
