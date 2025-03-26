using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerLocation : Location
{
    [SerializeField] string lobbySceneName;

    public string LobbySceneName => lobbySceneName; 
}
