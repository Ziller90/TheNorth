using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float baseDamage;
    public Transform hostCreature;
    public bool isCuttingAnimation;
    public FractionMarker thisCreatureFractionMarker;
    public MeleeWeaponSounds meleeWeaponSounds;
    
    public void SetCuttingAnimation(bool isCutting)
    {
        isCuttingAnimation = isCutting;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isCuttingAnimation)
        {
            meleeWeaponSounds.PlayHitSound();
            if (other.gameObject.tag == "HitBox")
            {
                if (other.gameObject.GetComponent<HitBox>().thisCreature != hostCreature && other.gameObject.GetComponent<HitBox>().thisCreature.GetComponent<FractionMarker>().creatureFraction != thisCreatureFractionMarker.creatureFraction)
                    other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage);
            }
        }
    }
}
