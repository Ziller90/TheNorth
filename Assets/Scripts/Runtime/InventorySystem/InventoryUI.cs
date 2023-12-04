using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemsManagerWindow inventoryWindow;
    [SerializeField] ItemsManagerWindow openedContainerWindow;

    [SerializeField] GameObject mobileBattleSystemInterface;
    [SerializeField] OpenedContainerView openedContainerView;

    InteractablesLocator playerInteractablesLocator;
    ContainerBody openedContainer;

    void OnEnable()
    {
        playerInteractablesLocator = Links.instance.playerCharacter.GetComponent<InteractablesLocator>();
        playerInteractablesLocator.containerOpenedEvent += OpenContainer;
    }

    void OnDisable()
    {
        playerInteractablesLocator.containerOpenedEvent -= OpenContainer;
    }

    public void OpenInventory()
    {
        Links.instance.currentItemsViewManager = inventoryWindow;

        inventoryWindow.gameObject.SetActive(true);
        mobileBattleSystemInterface.SetActive(false);
    }

    public void CloseInventory()
    {
        Links.instance.currentItemsViewManager = null;

        inventoryWindow.gameObject.SetActive(false);
        mobileBattleSystemInterface.SetActive(true);
    }

    public void OpenContainer(ContainerBody containerBody)
    {
        Links.instance.currentItemsViewManager = openedContainerWindow;

        openedContainer = containerBody;
        openedContainerView.SetOpenedContainer(openedContainer.GetComponent<SimpleContainer>());
        openedContainer.GetComponent<InteractableObject>().SetInteractable(false);

        mobileBattleSystemInterface.SetActive(false);
        openedContainerWindow.gameObject.SetActive(true);
    }

    public void CloseContainer()
    {
        Links.instance.currentItemsViewManager = null;

        openedContainer.CloseContainer();
        openedContainer.GetComponent<InteractableObject>().SetInteractable(true);

        openedContainerWindow.gameObject.SetActive(false);
        mobileBattleSystemInterface.SetActive(true);
    }
}
