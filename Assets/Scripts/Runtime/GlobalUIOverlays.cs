using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUIOverlays : MonoBehaviour
{
    [SerializeField] GameObject consoleView;
    [SerializeField] GameObject openConsoleButton;

    public void Start()
    {
        HideConsoleView();
    }

    public void ShowConsoleView()
    {
        consoleView.SetActive(true);
        openConsoleButton.SetActive(false);
    }

    public void HideConsoleView()
    {
        consoleView.SetActive(false);
        openConsoleButton.SetActive(true);
    }
}
