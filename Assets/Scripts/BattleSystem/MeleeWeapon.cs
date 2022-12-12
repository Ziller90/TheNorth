using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public FractionMarker thisCreatureFractionMarker;
    public MeleeWeaponSounds meleeWeaponSounds;
    public Health thisCreatureHealth;

    [SerializeField] float baseDamage;
    [SerializeField] Transform hostCreature;

    bool isCuttingAnimation;
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
                if (other.gameObject.GetComponent<HitBox>().thisCreature != hostCreature)
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
