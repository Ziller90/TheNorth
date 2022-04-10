using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour
{
    GlobalAims globalAims;
    void Start()
    {
        globalAims = LinksContainer.instance.globalAims;
        globalAims.globalAimsList.Add(gameObject.transform.position);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
