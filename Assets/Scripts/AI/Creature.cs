using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] GameObject thisCreature;
    [SerializeField] GameObject thisCreatureModel;
    [SerializeField] Collider thisCreatureCollider;
    [SerializeField] Behaviour[] components;
    [SerializeField] Health health;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioSource deathAudioSource;

    GlobalLists globalLists;

    void Start()
    {
        globalLists = Links.instance.globalLists;
        globalLists.creaturesOnLocation.Add(gameObject.transform);
        health.dieEvent += Die;
    } 
    public void Die()
    {
        globalLists.creaturesOnLocation.Remove(gameObject.transform);
        thisCreatureCollider.enabled = false;
        rigidbody.isKinematic = true;
        Destroy(healthBar);
        deathAudioSource.Play();
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
    }
}
