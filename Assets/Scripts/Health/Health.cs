using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public event Action dieEvent;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dieEvent();
        }
    }
}
