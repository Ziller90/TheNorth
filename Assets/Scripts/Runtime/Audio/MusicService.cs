using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicService : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void SetMusicTrack(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();    
    }
} 
