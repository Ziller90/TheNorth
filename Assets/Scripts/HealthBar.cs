using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarPrefab;
    Camera camera;
    HealthBarView healthBarView;
    GameObject healthBar;
    public Transform thisCreature;
    public Vector3 offset;
    public Health health;
    void Start()
    {
        camera = LinksContainer.instance.mainCamera;
        healthBar = Instantiate(healthBarPrefab, LinksContainer.instance.healthBarsContainer);
        healthBarView = healthBar.GetComponent<HealthBarView>();
    }
    void FixedUpdate()
    {
        healthBar.transform.position = camera.WorldToScreenPoint(thisCreature.position) + offset;
        healthBarView.SetBarFillness(health.currentHealth / health.maxHealth);
    }
    private void OnDestroy()
    {
        Destroy(healthBar);
    }
}
