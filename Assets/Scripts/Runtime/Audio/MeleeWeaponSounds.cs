using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MeleeWeaponSounds : MonoBehaviour
{
    [SerializeField] List<AudioClip> MeleeWeaponAirCuttingSounds;
    [SerializeField] List<AudioClip> MeleeWeaponHitObjectSounds;
    [SerializeField] List<AudioClip> MeleeWeaponHitCharacterSounds;

    [SerializeField] AudioMixerGroup audioMixerGroup;

    [SerializeField] MeleeWeapon meleeWeapon;

    private void OnEnable()
    {
        if (meleeWeapon != null)
        {
            meleeWeapon.airCutted += PlayAirCuttingSound;
            meleeWeapon.hittedObject += PlayHitObjectSound;
            meleeWeapon.hittedUnit += PlayHitCharacterSound;
        }
    }

    private void OnDisable()
    {
        if (meleeWeapon != null)
        {
            meleeWeapon.airCutted -= PlayAirCuttingSound;
            meleeWeapon.hittedObject -= PlayHitObjectSound;
            meleeWeapon.hittedUnit -= PlayHitCharacterSound;
        }
    }

    public void PlayAirCuttingSound()
    {
        Game.SoundService.PlaySound(MeleeWeaponAirCuttingSounds, transform.position, audioMixerGroup);
    }

    public void PlayHitObjectSound()
    {
        Game.SoundService.PlaySound(MeleeWeaponHitObjectSounds, transform.position, audioMixerGroup);
    }

    public void PlayHitCharacterSound()
    {
        Game.SoundService.PlaySound(MeleeWeaponHitCharacterSounds, transform.position, audioMixerGroup);
    }
}
