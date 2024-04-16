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
        if (!isCuttingAnimation) 
            return;

        ShieldBlock shieldBlock = other.gameObject.GetComponent<ShieldBlock>();
        if (shieldBlock != null)
        {
            if (shieldBlock.IsBlocking)
            {
                isCuttingAnimation = false;
                meleeWeaponSounds.PlayHitObjectSound();
            }
            return;
        }

        HitBox hitBox = other.gameObject.GetComponent<HitBox>();
        if (hitBox != null)
        {
            if (hitBox.Unit && hitBox.Unit != hostUnit)
            {
                meleeWeaponSounds.PlayHitCharacterSound();
                hitBox.HitBoxGetDamage(baseDamage, hostUnit.position);
            }
            return;
        }

        meleeWeaponSounds.PlayHitObjectSound();
    }
}
