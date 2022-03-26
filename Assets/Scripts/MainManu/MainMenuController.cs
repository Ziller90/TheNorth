using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainManuWindow;
    [SerializeField] GameObject optionWindow;
    [SerializeField] GameObject exitGameWindow;
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenOptionWindow()
    {
        mainManuWindow.SetActive(false);
        optionWindow.SetActive(true);
    }

    public void CloseOptionWindow()
    {
        mainManuWindow.SetActive(true);
        optionWindow.SetActive(false);
    }
    public void OpenExitGameWindow()
    {
        exitGameWindow.SetActive(true);
    }
    public void CloseExitGameWindow()
    {
        exitGameWindow.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if ( Input.GetKey(KeyCode.Escape) )
        {
            OpenExitGameWindow();
        }
    }
}
