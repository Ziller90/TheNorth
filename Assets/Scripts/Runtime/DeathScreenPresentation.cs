using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

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
        if (ScenesLauncher.isMultiplayer)
            PhotonNetwork.LeaveRoom();

        ScenesLauncher.LoadGameSceneWithLocation(locationToRespawnPrefab, spawnPoint);
    }

    public void GoToMainMenu()
    {
        if (ScenesLauncher.isMultiplayer)
            PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene(0);
    }
}
