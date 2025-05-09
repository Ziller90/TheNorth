using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EquipmentPosition
{
    RightHand,
    LeftHand,
    TwoHands,
    BothHands,
    Head,
    Torso,
    Legs,
    Feet,
    Spine
}

public class Equipment : MonoBehaviour
{
    [SerializeField] bool equipedCached;
    [SerializeField] List<Collider> itemColliders;
    [SerializeField] InteractableObject interactableObject;
    [SerializeField] EquipmentPosition equipmentPosition;

    public EquipmentPosition EquipmentPosition => equipmentPosition;

    Rigidbody rgbody;

    [ContextMenu("FindColliders")]
    public void FindItemColliders()
    {
        itemColliders = GetComponentsInChildren<Collider>().ToList();
    }

    void OnEnable()
    {
        interactableObject = GetComponent<InteractableObject>();
        rgbody = GetComponent<Rigidbody>();
        SetItemEquiped(equipedCached);
    }

    public void SetItemEquiped(bool isEquiped)
    {
        equipedCached = isEquiped;

        foreach (var collider in itemColliders)
        {
            collider.isTrigger = isEquiped;
        }

        if (rgbody)
            rgbody.isKinematic = equipedCached;

        if (interactableObject)
            interactableObject.SetInteractable(!isEquiped);
    }
}
