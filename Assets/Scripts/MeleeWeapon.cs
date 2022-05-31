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
    public Health thisCreatureHealth;

    public void Start()
    {
        if (thisCreatureHealth != null)
            thisCreatureHealth.dieEvent += Dead;
    }
    public void Dead()
    {
        isCuttingAnimation = false;
    }
    public void SetCuttingAnimation(bool isCutting)
    {
        isCuttingAnimation = isCutting;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isCuttingAnimation)
        {
            if (other.gameObject.tag == "Shield")
            {
                if (other.gameObject.GetComponent<ShieldBlock>().isBlocking == true)
                {
                    isCuttingAnimation = false;
                    meleeWeaponSounds.PlayHitObjectSound();
                }
            }
            else if (other.gameObject.tag == "HitBox")
            {
                if (other.gameObject.GetComponent<HitBox>().thisCreature != hostCreature && other.gameObject.GetComponent<HitBox>().thisCreature.GetComponent<FractionMarker>().creatureFraction != thisCreatureFractionMarker.creatureFraction)
                {
                    meleeWeaponSounds.PlayHitCharacterSound();
                    other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage, hostCreature.position);
                }
            }
            else
            {
                meleeWeaponSounds.PlayHitObjectSound();
            }
        }
    }
}
