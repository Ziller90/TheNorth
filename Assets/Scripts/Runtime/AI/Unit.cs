using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] GameObject thisUnit;
    [SerializeField] GameObject thisUnitView;
    [SerializeField] Collider thisUnitCollider;
    [SerializeField] Behaviour[] components;
    [SerializeField] Health health;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioSource deathAudioSource;

    GlobalLists globalLists;

    void Start()
    {
        globalLists = Links.instance.globalLists;
        globalLists.unitsOnLocation.Add(gameObject.transform);
        health.dieEvent += Die;
    } 
    public void Die()
    {
        globalLists.unitsOnLocation.Remove(gameObject.transform);
        thisUnitCollider.enabled = false;
        rigidbody.isKinematic = true;
        Destroy(healthBar);
        deathAudioSource.Play();
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
    }
}
