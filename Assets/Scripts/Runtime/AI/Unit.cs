using SiegeUp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Collider unitCollider;
    [SerializeField] Behaviour[] components;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioSource deathAudioSource;
    [SerializeField] string unitName;

    Health health;
    Rigidbody rigidbody;

    public string Name => unitName;

    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        Service<ActorsAccessModel>.Instance.RegisterUnit(gameObject.transform);
        health.dieEvent += Die;
    } 

    public void Die()
    {
        Game.ActorsAccessModel.RemoveUnitOnLocation(gameObject.transform);
        unitCollider.enabled = false;
        rigidbody.isKinematic = true;
        Destroy(healthBar);
        deathAudioSource.Play();
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
    }
}
