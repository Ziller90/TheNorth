using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType
{
    Singleplayer,
    DeathMatch
}

public class SceneLauncherService : MonoBehaviour
{
    GameObject locationToLoad;
    int spawnPoint;

    public GameObject LocationToLoad => locationToLoad;
    public int SpawnPoint => spawnPoint;

    public void SetSpawnPoint(int spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public void LoadGameSceneWithLocation(GameObject locationPrefab, int newSpawnPoint = -1)
    {
        locationToLoad = locationPrefab;
        spawnPoint = newSpawnPoint;
        SceneManager.LoadScene("GameScene");
    }
}
