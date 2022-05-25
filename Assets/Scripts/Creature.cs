using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    GlobalLists globalLists;
    public GameObject thisCreature;
    public GameObject thisCreatureModel;
    public Collider thisCreatureCollider;
    public Behaviour[] components;
    public Health health;
    public Rigidbody rigidbody;

    void Start()
    {
        globalLists = LinksContainer.instance.globalLists;
        globalLists.creaturesOnLocation.Add(gameObject.transform);
        health.dieEvent += Die;
    } 
    public void Die()
    {
        globalLists.creaturesOnLocation.Remove(gameObject.transform);
        thisCreatureCollider.enabled = false;
        rigidbody.isKinematic = true;
        for (int i = 0; i < components.Length; i++)
        {
            components[i].enabled = false;
        }
    }
}
