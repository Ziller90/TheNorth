using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksContainer : MonoBehaviour
{
    public GlobalAims globalAims;
    public static LinksContainer instance;
    public void Awake()
    {
        instance = this;
    }
}
