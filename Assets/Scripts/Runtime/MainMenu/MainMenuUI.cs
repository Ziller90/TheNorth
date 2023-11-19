using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public void Start()
    {
        playButton.onClick.AddListener(PlayButton);
        quitButton.onClick.AddListener(QuitButton);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GlobalMapScene");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
