using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener deathScreenOpener;
    [SerializeField] GameObject locationToRespawnPrefab;
    [SerializeField] int spawnPoint;

    void Start()
    {
        Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent += deathScreenOpener.ShowWindow;
    }

    public void Respawn()
    {
        Game.SceneLauncherService.LoadGameSceneWithLocation(locationToRespawnPrefab, spawnPoint);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
