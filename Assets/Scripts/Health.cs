using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public delegate void DieDelegate();
    public event DieDelegate dieEvent;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        dieEvent();
    }
}
