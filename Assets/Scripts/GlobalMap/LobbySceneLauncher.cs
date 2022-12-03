using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneLauncher : MonoBehaviour
{
    public void LoadLobbySceneWithLocation(int locationIndex)
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
