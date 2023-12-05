using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [SerializeField] AudioClip stepSound1;
    [SerializeField] AudioClip stepSound2;
    [SerializeField] AudioSource stepsAudioSource;
    [SerializeField] float runStepVolume;
    [SerializeField] float walkingStepVolume;

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
        {
            stepsAudioSource.volume = runStepVolume;
        }
        else
        {
            stepsAudioSource.volume = walkingStepVolume;
        }
        stepsAudioSource.Play();
    }
}
