using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("GlobalMapScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
