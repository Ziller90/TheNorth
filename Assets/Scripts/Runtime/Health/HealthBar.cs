using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthBarPrefab; 
    [SerializeField] Transform thisCreature;
    [SerializeField] Vector3 offset;
    [SerializeField] Health health;

    HealthBarView healthBarView;
    GameObject healthBar;
    Camera camera;
    void Start()
    {
        camera = Links.instance.mainCamera.GetComponent<Camera>();
        healthBar = Instantiate(healthBarPrefab, Links.instance.healthBarsContainer);
        healthBarView = healthBar.GetComponent<HealthBarView>();
    }
    void FixedUpdate()
    {
        healthBar.transform.position = camera.WorldToScreenPoint(thisCreature.position) + offset;
        healthBarView.SetBarFillness(health.CurrentHealth / health.MaxHealth);
    }
    private void OnDestroy()
    {
        Destroy(healthBar);
    }
}
