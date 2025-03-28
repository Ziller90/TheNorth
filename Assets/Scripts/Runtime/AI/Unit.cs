using Photon.Pun;
using SiegeUp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviourPunCallbacks
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
            if (component is not Health && component is not PhotonView && component is not PhotonTransformView)
                Destroy(component);
        }

        if (deathAnimator)
        {
            if (ScenesLauncher.isMultiplayer && GetComponent<PhotonView>().IsMine)
            {
                photonView.RPC("RPC_PlayDeathAnimation", RpcTarget.All, "Death", 0.20f, 0);
            }
            else if (!ScenesLauncher.isMultiplayer)
            {
                deathAnimator.CrossFadeInFixedTime("Death", 0.20f, 0);
            }
        }

        IsDead = true;
    }

    [PunRPC]
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
        if (ScenesLauncher.isMultiplayer)
            photonView.RPC("RPC_TryMoveFromSlotToSlotGroup", RpcTarget.All, container1, slot1, container2, slotGroup);
        else
            ModelUtils.TryMoveFromSlotToSlotGroup(container1, slot1, container2, slotGroup);
    }

    [PunRPC]
    public void RPC_TryMoveFromSlotToSlotGroup(ContainerBase container1, Slot slot1, ContainerBase container2, SlotGroup slotGroup)
    {
        ModelUtils.TryMoveFromSlotToSlotGroup(container1, slot1, container2, slotGroup);
    }
}
