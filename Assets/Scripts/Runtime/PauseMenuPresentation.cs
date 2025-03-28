using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenuPresentation : MonoBehaviourPunCallbacks
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

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenSaveGameMenu()
    {

    }

    public void OpenLoadGameMenu()
    {

    }
}
