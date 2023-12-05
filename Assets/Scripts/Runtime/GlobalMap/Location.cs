using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    ClickableObject clickableObject;
    [SerializeField] GameObject presentedLocationPrefab;
    public string Name => presentedLocationPrefab.GetComponent<LocationManager>().Name;
    public string Description => presentedLocationPrefab.GetComponent<LocationManager>().Description;
    public GameObject PresentedLocation => presentedLocationPrefab;

    private void Awake()
    {
        clickableObject = GetComponent<ClickableObject>();
    }
    private void OnEnable()
    {
        clickableObject.clickEvent += MoveToLocation;
    }
    private void OnDisable()
    {
        clickableObject.clickEvent -= MoveToLocation;
    }
    void MoveToLocation()
    {
        GlobalMapLinks.instance.playerSquad.MoveToLocation(gameObject);
    }
}
