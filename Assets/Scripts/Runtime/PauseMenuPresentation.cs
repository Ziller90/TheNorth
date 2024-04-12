using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener pauseMenu;

    public void OpenPauseMenuButton()
    {
        Time.timeScale = 0.0f;
        pauseMenu.ShowWindow();
    }

    public void ClosePauseMenuButton()
    {
        Time.timeScale = 1.0f;
    }
}
