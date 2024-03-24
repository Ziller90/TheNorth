using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField, Range(0, 24)] float timeOfDay;
    [SerializeField] float dayLengthInSeconds;
    public float TimeOfDay => timeOfDay;

    void Update()
    {
        timeOfDay += Time.deltaTime * (1 / (dayLengthInSeconds / 24));
        timeOfDay %= 24;
    }
}
