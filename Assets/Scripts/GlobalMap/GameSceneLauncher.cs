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
    static GameType locationToLoadGameType;
    public static GameType LocationToLoadGameType => locationToLoadGameType;
    public static GameObject LocationToLoad => locationToLoad;
    public void LoadGameSceneWithLocation(GameObject locationPrefab)
    {
        locationToLoadGameType = GameType.Singleplayer;
        locationToLoad = locationPrefab;
        SceneManager.LoadScene("GameScene");
    }
}
