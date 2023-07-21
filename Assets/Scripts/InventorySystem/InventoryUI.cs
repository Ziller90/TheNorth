using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject openedContainerWindow;
    [SerializeField] GameObject mobileIngameInterface;
    [SerializeField] OpenedContainerView openedContainerView;

    InteractablesLocator playerInteractablesLocator;
    ContainerBody openedContainer;

    void OnEnable()
    {
        playerInteractablesLocator = Links.instance.playerCharacter.GetComponent<InteractablesLocator>();
        playerInteractablesLocator.containerOpened += OpenContainer;
    }
    void OnDisable()
    {
        playerInteractablesLocator.containerOpened -= OpenContainer;
    }
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        mobileIngameInterface.SetActive(false);
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        mobileIngameInterface.SetActive(true);
    }
    public void OpenContainer(ContainerBody containerBody)
    {
        openedContainer = containerBody;
        openedContainer.OpenContainer();
        openedContainerView.SetOpenedContainer(openedContainer.GetComponent<Container>());
        openedContainer.GetComponent<InteractableObject>().SetInteractable(false);

        mobileIngameInterface.SetActive(false);
        openedContainerWindow.SetActive(true);
    }
    public void CloseContainer()
    {
        openedContainer.CloseContainer();
        openedContainer.GetComponent<InteractableObject>().SetInteractable(true);
        openedContainerWindow.SetActive(false);
        mobileIngameInterface.SetActive(true);
    }
}
