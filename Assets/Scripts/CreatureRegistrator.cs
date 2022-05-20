using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRegistrator : MonoBehaviour
{
    GlobalLists globalLists;
    void Start()
    {
        globalLists = LinksContainer.instance.globalLists;
        globalLists.creaturesOnLocation.Add(gameObject.transform);
    } 
}
