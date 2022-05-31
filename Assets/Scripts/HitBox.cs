using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public float hitBoxDamageModificator;
    public Transform thisCreature;
    public HumanoidBattleSystem battleSystem;
    public GameObject DebugPoint;
    public void HitBoxGetDamage(float damage, Vector3 hitPoint)
    {
        if (battleSystem.shieldRaised)
        {
            if (Vector3.Angle(-battleSystem.GetHitVector(hitPoint), thisCreature.forward) > battleSystem.shieldProtectionAngle / 2)
            {
                health.GetDamage(damage * hitBoxDamageModificator);
            }
        }
        else
        {
            health.GetDamage(damage * hitBoxDamageModificator);
        }
    }
}
