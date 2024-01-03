using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float baseDamage;

    MeleeWeaponSounds meleeWeaponSounds;
    Health thisUnitHealth;
    Transform hostUnit;
    bool isCuttingAnimation;

    public void SetWeaponHolder(Unit weaponHolder)
    {
        thisUnitHealth = weaponHolder.GetComponentInChildren<Health>();
        meleeWeaponSounds = weaponHolder.GetComponentInChildren<MeleeWeaponSounds>();
        hostUnit = weaponHolder.transform;

        if (thisUnitHealth != null)
            thisUnitHealth.dieEvent += () => SetCuttingAnimation(false);
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
                if (other.gameObject.GetComponent<HitBox>().Unit != hostUnit)
                {
                    meleeWeaponSounds.PlayHitCharacterSound();
                    other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage, hostUnit.position);
                }
            }
            else
            {
                meleeWeaponSounds.PlayHitObjectSound();
            }
        }
    }
}
