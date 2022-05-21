using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }
}
