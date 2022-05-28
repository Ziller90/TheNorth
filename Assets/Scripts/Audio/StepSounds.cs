using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    public AudioClip stepSound1;
    public AudioClip stepSound2;
    public AudioSource stepsAudioSource;
    public float runStepVolume;
    public float walkingStepVolume;

    public bool isRunning;

    public void SetStepSound1()
    {
        stepsAudioSource.clip = stepSound1;
    }
    public void SetStepSound2()
    {
        stepsAudioSource.clip = stepSound2;
    }
    public void PlaySound()
    {
        if (isRunning)
            stepsAudioSource.volume = runStepVolume;
        if (!isRunning)
            stepsAudioSource.volume = walkingStepVolume;
        stepsAudioSource.Play();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
