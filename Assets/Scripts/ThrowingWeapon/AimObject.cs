using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour
{
    GlobalLists globalAims;
    void Start()
    {
        globalAims = LinksContainer.instance.globalLists;
        globalAims.aimsOnLocation.Add(gameObject.transform);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
