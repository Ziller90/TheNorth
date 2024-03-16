using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    GameObject gameServicesRoot;
    [SerializeField] GlobalUIOverlays globalUIOverlaysPrefab;

    GlobalUIOverlays globalUIOverlays;

    private void Awake()
    {
        gameServicesRoot = new GameObject("gameServicesRoot");
        DontDestroyOnLoad(gameServicesRoot);

        globalUIOverlays = Instantiate(globalUIOverlaysPrefab, gameServicesRoot.transform);
        SceneManager.LoadScene("MainMenuScene");
    }
}
