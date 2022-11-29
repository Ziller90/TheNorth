using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMap : MonoBehaviour
{
    static int locationToLoad;
    public static int LocationToLoad => locationToLoad;
    public void LoadGameSceneWithLocation(int locationIndex)
    {
        locationToLoad = locationIndex;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadLobbySceneWithLocation(int locationIndex)
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
