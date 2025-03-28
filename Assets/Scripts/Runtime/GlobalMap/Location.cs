using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] GameObject presentedLocationPrefab;
    [SerializeField] bool isMultiplayer;

    ClickableObject clickableObject;

    public string Name => presentedLocationPrefab.GetComponent<LocationModel>().Name;
    public GameObject PresentedLocation => presentedLocationPrefab;
    public bool IsMultiplayer => isMultiplayer;

    void Awake()
    {
        clickableObject = GetComponent<ClickableObject>();
    }

    void OnEnable()
    {
        clickableObject.clickEvent += MoveToLocation;
    }

    void OnDisable()
    {
        clickableObject.clickEvent -= MoveToLocation;
    }

    void MoveToLocation()
    {
        GlobalMapLinks.instance.playerSquad.MoveToLocation(this);
    }
}
