using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject openedContainerWindow;
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
        inventoryPanel.SetActive(true);
        mobileBattleSystemInterface.SetActive(false);
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        mobileBattleSystemInterface.SetActive(true);
    }
    public void OpenContainer(ContainerBody containerBody)
    {
        openedContainer = containerBody;
        openedContainer.OpenContainer();
        openedContainerView.SetOpenedContainer(openedContainer.GetComponent<Container>());
        openedContainer.GetComponent<InteractableObject>().SetInteractable(false);

        mobileBattleSystemInterface.SetActive(false);
        openedContainerWindow.SetActive(true);
    }
    public void CloseContainer()
    {
        openedContainer.CloseContainer();
        openedContainer.GetComponent<InteractableObject>().SetInteractable(true);
        openedContainerWindow.SetActive(false);
        mobileBattleSystemInterface.SetActive(true);
    }
}
