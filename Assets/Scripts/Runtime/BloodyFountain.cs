using SiegeUp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyFountain : MonoBehaviour
{
    [SerializeField] int healthRegeneration;

    public Action fountainUsed;
    public void Drink(GameObject gameObject)
    {
        var health = gameObject.GetComponent<Health>(); 
        if (health != null)
        {
            health.GetComponent<Health>().Heal(healthRegeneration);
            fountainUsed?.InvokeSafe();
        }
    }
}
