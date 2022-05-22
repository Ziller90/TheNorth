using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float baseDamage;
    public Transform hostCreature;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            if (other.gameObject.GetComponent<HitBox>().thisCreature != hostCreature)
                other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage);
        }
    }
}
