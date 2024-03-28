using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] GameObject rainParticles;
    [SerializeField] bool RainOn;

    void Update()
    {
        rainParticles.SetActive(RainOn);
    }
}
