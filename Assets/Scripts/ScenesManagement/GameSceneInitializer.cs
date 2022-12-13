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

    public UnityAction sceneInitialized;

    public void Start()
    {
        InitializeScene();
    }
    public void InitializeScene()
    {
        Links.instance.locationLoader.LoadLocation();
        CreatePlayerCharacter();
        if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch && PhotonNetwork.IsMasterClient)
        {
            InstantiatePhotonItemsOnLocation();
        }
        sceneInitialized?.Invoke();
    }
    public void CreatePlayerCharacter()
    {
        GameObject playerCharacter = new GameObject();
        if (GameSceneLauncher.LocationToLoadGameType == GameType.Singleplayer)
        {
            playerCharacter = Instantiate(Humanoid, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity);
            SetMainCharacter(playerCharacter);
        }
        else if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch)
        {
            Debug.Log("Your ID in room is" + PhotonNetwork.LocalPlayer.ActorNumber);

            playerCharacter = PhotonNetwork.Instantiate(MultiplayerHumanoid.name, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity, 0);
            if (playerCharacter.GetComponent<PhotonView>().IsMine)
            {
                SetMainCharacter(playerCharacter);
            }
        }
    }
    public void SetMainCharacter(GameObject character)
    {
        Links.instance.mainCamera.GetComponent<CameraFollowing>().SetObjectToFollow(character);

        ActionManager characterActionManager = character.GetComponentInChildren<ActionManager>();
        ControlManager characterControlManager = character.GetComponentInChildren<ControlManager>();

        Links.instance.fixedJoystick.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetActionManager(characterActionManager);
        Links.instance.mobileButtonsManager.SetActionManager(characterActionManager);
        Links.instance.playerCharacter = character;
        character.GetComponentInChildren<Health>().dieEvent += Links.instance.deathScreen.ActivateDeathScreen;
    }
    public void InstantiatePhotonItemsOnLocation()
    {
        Debug.Log("Number of items on location" + Links.instance.globalLists.itemsOnLocation.Count);
        foreach(Transform itemTransfrom in Links.instance.globalLists.itemsOnLocation)
        {
            PhotonNetwork.Instantiate(itemTransfrom.gameObject.GetComponent<Item>().itemData.prefab.name, itemTransfrom.position, itemTransfrom.rotation);
            Destroy(itemTransfrom.gameObject);
        }
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
