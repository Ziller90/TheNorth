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
    static int locationToLoad;
    static GameType locationToLoadGameType;
    public static GameType LocationToLoadGameType => locationToLoadGameType;
    public static int LocationToLoad => locationToLoad;
    public void LoadGameSceneWithLocation(int locationIndex)
    {
        locationToLoadGameType = GameType.Singleplayer;
        locationToLoad = locationIndex;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadLobbySceneWithLocation(int locationIndex, GameType gameType)
    {
        locationToLoadGameType = gameType;
        locationToLoad = locationIndex;
        SceneManager.LoadScene("LobbyScene");
    }
}
