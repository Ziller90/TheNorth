using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float hitBoxDamageModificator;
    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] GameObject DebugPoint;

    public Transform thisCreature;
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
