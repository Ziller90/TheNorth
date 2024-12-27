using SiegeUp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyFountainView : MonoBehaviour
{
    [SerializeField] BloodyFountain bloodyFountain;
    [SerializeField] ParticleSystem fountainParticleSystem;
    [SerializeField] GameObject bloodObject;

    void OnEnable()
    {
        bloodyFountain.fountainUsed += OnFountainUsed;
    }

    void OnDisable()
    {
        bloodyFountain.fountainUsed += OnFountainUsed;
    }

    void OnFountainUsed()
    {
        bloodObject.SetActive(false);
        fountainParticleSystem.Stop();
    }
}
