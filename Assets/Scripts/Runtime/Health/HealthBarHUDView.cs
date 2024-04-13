using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarHUDView : HUDViewBase
{
    [SerializeField] Gradient gradient;
    [SerializeField] Transform damageIndicatorBarTransform;
    [SerializeField] Transform healthBarTransform;
    [SerializeField] Image healthBarImage;
    [SerializeField] float Speed;

    Health health;

    float healthBarFillness = 1;
    float damageIndicatorBarFillness = 1;

    public override bool IsVisible()
    {
        return health.CurrentHealth > 0;
    }

    public override void SetObservedObject(GameObject observedObject, HUDPanelsView hudPanelsView = null)
    {
        health = observedObject.GetComponent<Health>(); 
    }

    void Update()
    {
        var newBarFillness = health.CurrentHealth / health.MaxHealth;
        if (newBarFillness != damageIndicatorBarFillness)
        {
            damageIndicatorBarFillness = Mathf.SmoothStep(damageIndicatorBarFillness, newBarFillness, Speed);
            healthBarFillness = newBarFillness;
        }
        healthBarFillness = Mathf.Clamp(healthBarFillness, 0, 1);
        healthBarTransform.localScale = new Vector3(healthBarFillness, 1, 1);

        damageIndicatorBarFillness = Mathf.Clamp(damageIndicatorBarFillness, 0, 1);
        damageIndicatorBarTransform.localScale = new Vector3(damageIndicatorBarFillness, 1, 1); 
        healthBarImage.color = gradient.Evaluate(healthBarFillness);
    }
}
