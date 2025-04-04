using SiegeUp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Collider unitCollider;
    [SerializeField] Behaviour[] components;
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
        deathAudioSource.Play();

        if (!ScenesLauncher.isMultiplayer)
            CreateDeadBodyContainer();

        foreach (Behaviour component in components)
        {
            Destroy(component);
        }

        if (deathAnimator)
        {
            deathAnimator.CrossFadeInFixedTime("Death", 0.20f, 0);

        }

        IsDead = true;
    }

    private void RPC_PlayDeathAnimation(string animationName, float transitionDuration, int layer)
    {
        deathAnimator.CrossFadeInFixedTime(animationName, transitionDuration, layer);
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
                TryMoveFromSlotToSlotGroupSync(humanoidInventroyContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

            foreach (var slot in humanoidInventroyContainer.QuickAccessSlots.Slots)
                TryMoveFromSlotToSlotGroupSync(humanoidInventroyContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);

            TryMoveFromSlotToSlotGroupSync(humanoidInventroyContainer, humanoidInventroyContainer.MainWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);
            TryMoveFromSlotToSlotGroupSync(humanoidInventroyContainer, humanoidInventroyContainer.SecondaryWeaponSlot, deadBodyContainer, deadBodyContainer.SlotGroup);
        }
        else if (simpleContainer != null)
        {
            foreach (var slot in simpleContainer.SlotGroup.Slots)
                TryMoveFromSlotToSlotGroupSync(simpleContainer, slot, deadBodyContainer, deadBodyContainer.SlotGroup);
        }

        Destroy(unitContainer);
    }

    public void TryMoveFromSlotToSlotGroupSync(ContainerBase container1, Slot slot1, ContainerBase container2, SlotGroup slotGroup)
    {
        ModelUtils.TryMoveFromSlotToSlotGroup(container1, slot1, container2, slotGroup);
    }

    public void RPC_TryMoveFromSlotToSlotGroup(ContainerBase container1, Slot slot1, ContainerBase container2, SlotGroup slotGroup)
    {
        ModelUtils.TryMoveFromSlotToSlotGroup(container1, slot1, container2, slotGroup);
    }
}
