using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarView : MonoBehaviour
{
    public float barFillness;
    public Gradient gradient;
    public Transform barLineTransform;
    public RawImage barLine;
    public float newBarFillness;
    public float Speed;

    public void SetBarFillness(float newBarFillness)
    {
        this.newBarFillness = newBarFillness;
    }
    void Update()
    {
        if (newBarFillness != barFillness)
        {
            barFillness = Mathf.SmoothStep(barFillness, newBarFillness, Speed);
        }
        barFillness = Mathf.Clamp(barFillness, 0, 1);
        barLineTransform.localScale = new Vector3(barFillness, 1, 1); 
        barLine.color = gradient.Evaluate(barFillness);
    }
}
