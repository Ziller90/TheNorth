using SiegeUp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Collider unitCollider;
    [SerializeField] Behaviour[] components;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioSource deathAudioSource;
    [SerializeField] string unitName;

    [SerializeField] GameObject unitBodyView;
    [SerializeField] HumanoidInventoryContainer inventoryContainer;

    Health health;
    Rigidbody rigidbody;
    SimpleContainer deadBodyContainer;

    public SimpleContainer DeadBodyContainer => deadBodyContainer;
    public bool IsDead { get; private set; }
    
    public string Name => unitName;

    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        Service<ActorsAccessModel>.Instance.RegisterUnit(gameObject.transform);
        health.dieEvent += Die;
    } 

    public void Die()
    {
        Game.ActorsAccessModel.RemoveUnitOnLocation(gameObject.transform);
        unitCollider.enabled = false;
        rigidbody.isKinematic = true;
        Destroy(healthBar);
        deathAudioSource.Play();
        CreateDeadBodyContainer();
        foreach (Behaviour component in components)
        {
            Destroy(component);
        }
        IsDead = true;
    }

    public void CreateDeadBodyContainer()
    {
        if (Game.GameSceneInitializer.Player == gameObject)
            return; 

        deadBodyContainer = unitBodyView.AddComponent<SimpleContainer>();
        deadBodyContainer.InitializeSlotGroup(30);

        var interactableObject = unitBodyView.AddComponent<InteractableObject>();
        var interactableObjectView = unitBodyView.AddComponent<InteractableObjectView>();
        interactableObjectView.FindObjectMeshRenderers();
        interactableObjectView.SetInteractableObject(interactableObject);

        var containerBody = unitBodyView.AddComponent<ContainerBody>();
        containerBody.SetIsDisposable(true);    
        containerBody.SetContainer(deadBodyContainer);
        containerBody.SetDestroyOnDispose(false);
        containerBody.SetComponentsToDeleteOnDispose(containerBody, interactableObject, interactableObjectView);

        foreach (var slot in inventoryContainer.BackpackSlots.Slots)
            ModelUtils.TryMoveFromSlotToSlotGroup(inventoryContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

        foreach (var slot in inventoryContainer.QuickAccessSlots.Slots)
            ModelUtils.TryMoveFromSlotToSlotGroup(inventoryContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

        ModelUtils.TryMoveFromSlotToSlotGroup(inventoryContainer, inventoryContainer.MainWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);
        ModelUtils.TryMoveFromSlotToSlotGroup(inventoryContainer, inventoryContainer.SecondaryWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);

        Destroy(inventoryContainer);
    }
}
