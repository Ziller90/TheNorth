using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource customAudioSource;

    public void Play(AudioClip audioClip = null)
    {
        if (audioClip)
        {
            customAudioSource.clip = audioClip;
            customAudioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        audioSource.volume = 1;
        customAudioSource.volume = 1;
    }
}
