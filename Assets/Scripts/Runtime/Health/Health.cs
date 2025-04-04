using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SiegeUp.Core;

[System.Serializable, ComponentId(3)]
public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField, AutoSerialize(1)] float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public event Action dieEvent;

    bool isDead = false;

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dieEvent();
        }
    }

    public void Heal(float hp)
    {
        currentHealth += hp;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
