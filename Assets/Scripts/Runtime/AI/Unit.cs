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
    [SerializeField] ContainerBase unitContainer;
    [SerializeField] Animator deathAnimator;

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

    void OnEnable()
    {
        if (health)
        {
            health.dieEvent -= Die;
            health.dieEvent += Die;
        }
    }

    void OnDisable()
    {
        if (health)
            health.dieEvent -= Die;
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

        if (deathAnimator)
            deathAnimator.CrossFadeInFixedTime("Death", 0.20f, 0);

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

        var humanoidInventroyContainer = unitContainer as HumanoidInventoryContainer;
        var simpleContainer = unitContainer as SimpleContainer;

        if (humanoidInventroyContainer != null)
        {
            foreach (var slot in humanoidInventroyContainer.BackpackSlots.Slots)
                ModelUtils.TryMoveFromSlotToSlotGroup(humanoidInventroyContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

            foreach (var slot in humanoidInventroyContainer.QuickAccessSlots.Slots)
                ModelUtils.TryMoveFromSlotToSlotGroup(humanoidInventroyContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

            ModelUtils.TryMoveFromSlotToSlotGroup(humanoidInventroyContainer, humanoidInventroyContainer.MainWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);
            ModelUtils.TryMoveFromSlotToSlotGroup(humanoidInventroyContainer, humanoidInventroyContainer.SecondaryWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);
        }
        else if (simpleContainer != null)
        {
            foreach (var slot in simpleContainer.SlotGroup.Slots)
                ModelUtils.TryMoveFromSlotToSlotGroup(simpleContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);
        }

        Destroy(unitContainer);
    }
}
