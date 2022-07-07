using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    public List<GameObject> locationsList;
    public int locationToLoadIndex;
    void Start()
    {
        Instantiate(locationsList[locationToLoadIndex], gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
