using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    ClickableObject clickableObject;
    [SerializeField] string name;

    public string Name => name;
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
