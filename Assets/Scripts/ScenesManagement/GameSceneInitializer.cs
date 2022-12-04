using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class GameSceneInitializer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Humanoid;
    [SerializeField] GameObject MultiplayerHumanoid;

    ControlManager playerControlManager;
    ActionManager playerActionManager;
    public UnityAction sceneInitialized;

    public void Start()
    {
        playerActionManager = Links.instance.playerActionManager;
        playerControlManager = Links.instance.playerControlManager;
        InitializeScene();
    }
    public void InitializeScene()
    {
        Links.instance.locationLoader.LoadLocation();
        CreatePlayerCharacter();
        sceneInitialized?.Invoke();
    }
    public void CreatePlayerCharacter()
    {
        GameObject playerCharacter = new GameObject();
        if (GameSceneLauncher.LocationToLoadGameType == GameType.Singleplayer)
        {
            playerCharacter = Instantiate(Humanoid, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity);
        }
        else if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch)
        {
            GameObject newPlayer = PhotonNetwork.Instantiate(MultiplayerHumanoid.name, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity, 0);
            Debug.Log("Your ID in room is" + PhotonNetwork.LocalPlayer.ActorNumber);
        }
        Links.instance.mainCamera.GetComponent<CameraFollowing>().SetObjectToFollow(playerCharacter);
        playerCharacter.GetComponent<CharacterContoller>().SetControlManager(playerControlManager);
        playerCharacter.GetComponent<HumanoidBattleSystem>().SetActionManager(playerActionManager);
        playerCharacter.GetComponentInChildren<ItemsCollector>().SetActionManager(playerActionManager);
        Links.instance.playerCharacter = playerCharacter;
    }
    public void LeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("LobbyScene");
        }
        else
        {
            SceneManager.LoadScene("GlobalMapScene");
        }
        PhotonNetwork.LeaveRoom();
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(other.NickName + " entered the room " + PhotonNetwork.CurrentRoom);
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log(other.NickName + " left the room " + PhotonNetwork.CurrentRoom);
    }
}
