using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangdollManager : MonoBehaviour
{
    public Health health;

    private void Start()
    {
        health.dieEvent += ActivateRangdoll;
    }
    public void ActivateRangdoll()
    {
        Debug.Log("Rangdoll activated");
    }
}
