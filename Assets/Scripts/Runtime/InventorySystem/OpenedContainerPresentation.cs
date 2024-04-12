using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedContainerPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener containerWindowOpener;

    InteractablesLocator playerInteractablesLocator;
    ContainerBody openedContainerBody;

    SimpleContainer openedContainer;
    HumanoidInventoryContainer playerInventory;

    public SimpleContainer OpenedContainer => openedContainer;
    public HumanoidInventoryContainer PlayerInventory => playerInventory;

    void OnEnable()
    {
        playerInventory = Game.GameSceneInitializer.Player.GetComponentInChildren<HumanoidInventoryContainer>();
        playerInteractablesLocator = Game.GameSceneInitializer.Player.GetComponent<InteractablesLocator>();

        playerInteractablesLocator.containerOpenedEvent += OpenContainer;
    }

    void OnDisable()
    {
        playerInteractablesLocator.containerOpenedEvent -= OpenContainer;
    }

    public void OpenContainer(ContainerBody containerBody)
    {
        openedContainerBody = containerBody;
        openedContainer = containerBody.GetComponent<SimpleContainer>();    
        openedContainer.GetComponent<InteractableObject>().SetInteractable(false);
        containerWindowOpener.ShowWindow();
    }

    public void CloseContainer()
    {
        openedContainerBody.CloseContainer();
        openedContainer.GetComponent<InteractableObject>().SetInteractable(true);
    }
}
