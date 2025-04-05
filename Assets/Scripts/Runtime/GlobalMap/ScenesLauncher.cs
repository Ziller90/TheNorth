using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType
{
    Singleplayer,
    DeathMatch
}

public class ScenesLauncher : MonoBehaviour
{
    static GameObject locationToLoad;
    public static GameObject LocationToLoad => locationToLoad;
    public static int spawnPoint;

    public static void LoadGameSceneWithLocation(GameObject locationPrefab, int newSpawnPoint = -1)
    {
        locationToLoad = locationPrefab;
        spawnPoint = newSpawnPoint;
        SceneManager.LoadScene("GameScene");
    }
}
