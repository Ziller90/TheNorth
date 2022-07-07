using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] MeleeWeaponAirCuttingSounds;
    [SerializeField] AudioClip[] MeleeWeaponHitObjectSounds;
    [SerializeField] AudioClip[] MeleeWeaponHitCharacterSounds;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeBetweenHitSounds;

    bool alreadyPlayedHitSound;
    
    public void PlayAirCuttingSound()
    {
        int randomNumber = Random.Range(0, MeleeWeaponAirCuttingSounds.Length);
        audioSource.clip = MeleeWeaponAirCuttingSounds[randomNumber];
        audioSource.Play();
    }
    public void PlayHitObjectSound()
    {
        if (alreadyPlayedHitSound == false)
        {
            int randomNumber = Random.Range(0, MeleeWeaponHitObjectSounds.Length);
            audioSource.clip = MeleeWeaponHitObjectSounds[randomNumber];
            audioSource.Play();
            alreadyPlayedHitSound = true;
            StartCoroutine("WaitTillNextSound");
        }
    }
    public void PlayHitCharacterSound()
    {
        if (alreadyPlayedHitSound == false)
        {
            int randomNumber = Random.Range(0, MeleeWeaponHitCharacterSounds.Length);
            audioSource.clip = MeleeWeaponHitCharacterSounds[randomNumber];
            audioSource.Play();
            alreadyPlayedHitSound = true;
            StartCoroutine("WaitTillNextSound");
        }
    }
    public IEnumerator WaitTillNextSound()
    {
        yield return new WaitForSeconds(timeBetweenHitSounds);
        alreadyPlayedHitSound = false;
    }
}
