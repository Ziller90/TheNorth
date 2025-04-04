using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float hitBoxDamageModificator;
    [SerializeField] FightManager battleSystem;
    [SerializeField] GameObject DebugPoint;
    [SerializeField] Transform unit;

    public Transform Unit => unit;

    public void HitBoxGetDamage(float damage, Vector3 hitPoint)
    {
        if (battleSystem.ShieldRaised)
        {
            if (Vector3.Angle(-battleSystem.GetHitVector(hitPoint), unit.forward) > battleSystem.ShieldProtectionAngle / 2)
            {
                health.GetDamage(damage);
            }
        }
        else
        {
            health.GetDamage(damage);
        }
    }
}
