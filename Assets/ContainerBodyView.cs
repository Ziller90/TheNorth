using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContainerBody))]
public class ContainerBodyView : MonoBehaviour
{
    [SerializeField] Animator containerAnimator;
    ContainerBody containerBody;
    private void Start()
    {
        containerBody = GetComponent<ContainerBody>();
        containerBody.containerOpened.AddListener(OpenContainer);
        containerBody.containerClosed.AddListener(CloseContainer);
    }
    public void OpenContainer()
    {
        containerAnimator.SetBool("Opened", true);
    }
    public void CloseContainer()
    {
        containerAnimator.SetBool("Opened", false);
    }
}
