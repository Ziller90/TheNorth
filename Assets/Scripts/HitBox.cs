using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public float hitBoxDamageModificator;
    public Transform thisCreature;
    public void HitBoxGetDamage(float damage)
    {
        health.GetDamage(damage * hitBoxDamageModificator);
    }
}
