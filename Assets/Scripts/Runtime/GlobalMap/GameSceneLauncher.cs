using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType
{
    Singleplayer,
    DeathMatch
}
public class GameSceneLauncher : MonoBehaviour
{
    static GameObject locationToLoad;
    public static GameObject LocationToLoad => locationToLoad;
    public void LoadGameSceneWithLocation(GameObject locationPrefab)
    {
        locationToLoad = locationPrefab;
        SceneManager.LoadScene("GameScene");
    }
}
