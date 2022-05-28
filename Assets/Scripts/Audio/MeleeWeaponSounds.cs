using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSounds : MonoBehaviour
{
    public AudioClip[] MeleeWeaponAirCuttingSounds;
    public AudioClip[] MeleeWeaponHitSounds;
    public AudioSource audioSource;
    public void PlayAirCuttingSound()
    {
        int randomNumber = Random.Range(0, MeleeWeaponAirCuttingSounds.Length);
        audioSource.clip = MeleeWeaponAirCuttingSounds[randomNumber];
        audioSource.Play();
    }
    public void PlayHitSound()
    {
        int randomNumber = Random.Range(0, MeleeWeaponHitSounds.Length);
        audioSource.clip = MeleeWeaponHitSounds[randomNumber];
        audioSource.Play();
    }
}
