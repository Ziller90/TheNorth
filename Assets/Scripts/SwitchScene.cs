using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] string nameScene;
    public void LoadNewScene()
    {
        SceneManager.LoadScene(nameScene);
    }
}
