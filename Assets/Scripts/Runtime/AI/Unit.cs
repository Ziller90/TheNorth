using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Collider unitCollider;
    [SerializeField] Behaviour[] components;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioSource deathAudioSource;

    Health health;
    Rigidbody rigidbody;
    GlobalLists globalLists;

    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        globalLists = Links.instance.globalLists;
        globalLists.unitsOnLocation.Add(gameObject.transform);
        health.dieEvent += Die;
    } 
    public void Die()
    {
        globalLists.unitsOnLocation.Remove(gameObject.transform);
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