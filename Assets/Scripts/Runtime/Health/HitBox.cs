using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float hitBoxDamageModificator;
    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] GameObject DebugPoint;
    [SerializeField] Transform thisCreature;
    public Transform ThisCreature => thisCreature;
    public void HitBoxGetDamage(float damage, Vector3 hitPoint)
    {
        if (battleSystem.ShieldRaised)
        {
            if (Vector3.Angle(-battleSystem.GetHitVector(hitPoint), thisCreature.forward) > battleSystem.ShieldProtectionAngle / 2)
                health.GetDamage(damage);
        }
        else
        {
            health.GetDamage(damage);
        }
    }
}
