using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationView : MonoBehaviour
{
    Location location;
    GameObject locationView;
    [SerializeField] GameObject locationInfoViewPrefab;
    [SerializeField] Transform locationInfoPosition;
    [SerializeField] Vector3 minZoomNameTextOffset;
    [SerializeField] Vector3 maxZoomNameTextOffset;    
    [SerializeField] float minZoomNameTextSize;
    [SerializeField] float maxZoomNameTextSize;
    [SerializeField] Color nameColor;

    public Location Location => location;
    public Vector3 MinZoomNameTextOffset => minZoomNameTextOffset;
    public Vector3 MaxZoomNameTextOffset => maxZoomNameTextOffset;
    public float MinZoomNameTextSize => minZoomNameTextSize;
    public float MaxZoomNameTextSize => maxZoomNameTextSize;
    public Transform LocationInfoPosition => locationInfoPosition;
    public Color NameColor => nameColor;
    void Start()
    {
        location = GetComponent<Location>();
        InstantiateLocationInfoView(location);
    }
    private void OnDisable()
    {
        DestroyLocationInfoView();
    }
    void InstantiateLocationInfoView(Location location)
    {
        locationView = Instantiate(locationInfoViewPrefab, GlobalMapLinks.instance.locationsInfoViewContainer.transform);
        locationView.GetComponent<LocationInfoText>().SetLocationView(this);
    }
    void DestroyLocationInfoView()
    {
        Destroy(locationView);
    }
}
