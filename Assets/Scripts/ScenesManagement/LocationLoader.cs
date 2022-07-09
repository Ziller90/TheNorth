using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> locationsList;
    [SerializeField] int locationToLoadIndex;
    void Start()
    {
        Instantiate(locationsList[locationToLoadIndex], gameObject.transform);
    }
}
