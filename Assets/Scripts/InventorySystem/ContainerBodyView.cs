using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBodyView : MonoBehaviour
{
    [SerializeField] Animator containerAnimator;
    ContainerBody containerBody;

    private void Start()
    {
        containerBody = transform.parent.GetComponent<ContainerBody>();
        containerBody.containerOpened += OpenContainer;
        containerBody.containerClosed += CloseContainer;
    }

    public void OpenContainer() => containerAnimator.SetBool("Opened", true);
    public void CloseContainer() => containerAnimator.SetBool("Opened", false);
}
