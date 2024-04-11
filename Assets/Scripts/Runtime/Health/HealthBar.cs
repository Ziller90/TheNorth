using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthBarPrefab; 
    [SerializeField] Transform thisUnit;
    [SerializeField] Vector3 offset;
    [SerializeField] Health health;

    HealthBarView healthBarView;
    GameObject healthBar;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Game.MainCameraService.MainCamera.GetComponent<Camera>();
        healthBar = Instantiate(healthBarPrefab, Links.instance.healthBarsContainer);
        healthBarView = healthBar.GetComponent<HealthBarView>();
    }

    void FixedUpdate()
    {
        healthBar.transform.position = mainCamera.WorldToScreenPoint(thisUnit.position) + offset;
        healthBarView.SetBarFillness(health.CurrentHealth / health.MaxHealth);
    }

    private void OnDestroy()
    {
        Destroy(healthBar);
    }
}
