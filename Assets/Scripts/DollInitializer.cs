using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DollInitializer : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] Behaviour[] redundatComponents;
    [SerializeField] GameObject[] redundatObjects;
    void Start()
    {
        if (character.GetComponent<PhotonView>().IsMine == false)
        {
            foreach(Behaviour component in redundatComponents)
            {
                component.enabled = false;
            }
            foreach (GameObject gameObject in redundatObjects)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
