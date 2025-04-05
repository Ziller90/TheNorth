using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class TimeService : MonoBehaviour
{
    [SerializeField, Range(0, 24)] float timeOfDay;
    [SerializeField] float dayLengthInSeconds;
    [SerializeField] bool dinamicGameTime;

    public float TimeOfDay => timeOfDay;

    void Update()
    {
        if (dinamicGameTime)
        {
            timeOfDay += Time.deltaTime * (1 / (dayLengthInSeconds / 24));
            timeOfDay %= 24;
        }
    }
}
