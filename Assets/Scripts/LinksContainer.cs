using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksContainer : MonoBehaviour
{
    public GlobalLists globalLists;
    public static LinksContainer instance;
    public void Awake()
    {
        instance = this;
    }
}
