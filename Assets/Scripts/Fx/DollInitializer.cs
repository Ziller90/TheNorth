using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollInitializer : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] Behaviour[] redundatComponents;
    [SerializeField] GameObject[] redundatObjects;
    void Start()
    {
        foreach (Behaviour component in redundatComponents)
        {
            component.enabled = false;
        }
        foreach (GameObject gameObject in redundatObjects)
        {
            Destroy(gameObject);
        }
    }
}
