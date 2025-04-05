using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SiegeUp.Core;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float baseDamage;

    MeleeWeaponSounds meleeWeaponSounds;
    Health thisUnitHealth;
    Transform hostUnit;
    bool isCuttingAnimation;
    bool hittedSomething;

    public Action hittedObject;
    public Action hittedUnit;
    public Action airCutted;

    public void SetWeaponHolder(Unit weaponHolder)
    {
        thisUnitHealth = weaponHolder.GetComponentInChildren<Health>();
        meleeWeaponSounds = weaponHolder.GetComponentInChildren<MeleeWeaponSounds>();
        hostUnit = weaponHolder.transform;

        var hostColliders = hostUnit.GetComponentsInChildren<Collider>();
        var meleeWeaponCollider = GetComponent<Collider>();

        foreach (var hostCollider in hostColliders)
            Physics.IgnoreCollision(meleeWeaponCollider, hostCollider);

        if (thisUnitHealth != null)
            thisUnitHealth.dieEvent += () => SetCuttingAnimation(false);
    }

    public void SetCuttingAnimation(bool isCutting)
    {
        isCuttingAnimation = isCutting;

        if (isCutting == true)
            hittedSomething = false;

        if (isCutting == false && hittedSomething == false) 
            airCutted?.InvokeSafe();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " collided with " + other.name + " timestep is " + Time.frameCount);
        if (!isCuttingAnimation) 
            return;

        hittedSomething = true;
             
        ShieldBlock shieldBlock = other.gameObject.GetComponent<ShieldBlock>();
        if (shieldBlock != null)
        {
            if (shieldBlock.IsBlocking)
            {
                isCuttingAnimation = false;
                hittedObject?.InvokeSafe();
            }
            return;
        }

        HitBox hitBox = other.gameObject.GetComponent<HitBox>();
        if (hitBox != null)
        {
            if (hitBox.Unit && hitBox.Unit != hostUnit)
            {
                hittedUnit?.InvokeSafe();
                hitBox.HitBoxGetDamage(baseDamage, hostUnit.position);
            }
            return;
        }

        hittedObject?.InvokeSafe();
    }
}
