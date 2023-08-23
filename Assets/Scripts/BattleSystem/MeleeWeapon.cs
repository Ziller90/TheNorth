using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float baseDamage;

    MeleeWeaponSounds meleeWeaponSounds;
    Health thisCreatureHealth;
    Transform hostCreature;
    bool isCuttingAnimation;

    public void SetWeaponHolder(Creature weaponHolder)
    {
        thisCreatureHealth = weaponHolder.GetComponentInChildren<Health>();
        meleeWeaponSounds = weaponHolder.GetComponentInChildren<MeleeWeaponSounds>();
        hostCreature = weaponHolder.transform;

        if (thisCreatureHealth != null)
            thisCreatureHealth.dieEvent += () => SetCuttingAnimation(false);
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
                if (other.gameObject.GetComponent<ShieldBlock>().IsBlocking == true)
                {
                    isCuttingAnimation = false;
                    meleeWeaponSounds.PlayHitObjectSound();
                }
            }
            else if (other.gameObject.tag == "HitBox")
            {
                if (other.gameObject.GetComponent<HitBox>().ThisCreature != hostCreature)
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
