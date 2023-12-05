using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationInfoText : MonoBehaviour
{
    [SerializeField] TMP_Text locationName;
    LocationView locationView;
    Camera mainCamera;
    Vector3 startLocationInfoSize;
    private void Awake()
    {
        mainCamera = GlobalMapLinks.instance.globalMapCamera;
        startLocationInfoSize = gameObject.transform.transform.localScale;
    }
    public void SetLocationView(LocationView location)
    {
        locationView = location;
        UpdateView();
    }
    void UpdateView()
    {
        locationName.text = locationView.Location.Name;
        locationName.color = locationView.NameColor;
        var locationScreenPosition = mainCamera.WorldToScreenPoint(locationView.LocationInfoPosition.position);
        var offset = Vector3.Lerp(locationView.MinZoomNameTextOffset, locationView.MaxZoomNameTextOffset, mainCamera.GetComponent<RTSCamera>().Zoom);
        var size = Mathf.Lerp(locationView.MinZoomNameTextSize, locationView.MaxZoomNameTextSize, mainCamera.GetComponent<RTSCamera>().Zoom);
        locationName.transform.position = locationScreenPosition + offset;
        locationName.transform.localScale = startLocationInfoSize * size;
    }
    public void Update()
    {
        UpdateView();
    }
}
