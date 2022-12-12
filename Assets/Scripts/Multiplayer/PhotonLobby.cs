using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] string region;
    [SerializeField] string gameVersion = "1";
    [SerializeField] int maxPlayersInRoom;
    [SerializeField] TMP_InputField roomNameToJoin;
    [SerializeField] TMP_InputField roomNameToCreate;

    [SerializeField] Button createRoomButton;
    [SerializeField] Button joinRoomButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Button leaveLobby;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        createRoomButton.onClick.AddListener(CreateRoomButton);
        joinRoomButton.onClick.AddListener(JoinRoomByName);
        startGameButton.onClick.AddListener(GoToGameScene);
        leaveLobby.onClick.AddListener(LeaveLobby);

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        Connect();
        PhotonNetwork.GameVersion = gameVersion;
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }
    public void JoinRoomByName()
    {
        if (!string.IsNullOrWhiteSpace(roomNameToJoin.text))
        {
            PhotonNetwork.JoinRoom(roomNameToJoin.text);
        }
        else
        {
            Debug.Log("Room to join name is empty field. Enter correct room name");
        }
    }
    public void CreateRoomButton()
    {
        if (!string.IsNullOrWhiteSpace(roomNameToCreate.text))
        {
            Debug.Log(string.IsNullOrWhiteSpace(roomNameToCreate.text));
            PhotonNetwork.CreateRoom(roomNameToCreate.text, new RoomOptions { MaxPlayers = (byte)maxPlayersInRoom });
        }
        else
        {
            Debug.Log("Room to create name is empty field. Enter correct room name");
        }
    }
    public void LeaveLobby()
    {
        SceneManager.LoadScene("GlobalMapScene");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("You are connected to " + PhotonNetwork.CloudRegion);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("You are disconnected!");
        Debug.Log(cause.ToString());
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("New room is created. Room name - " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error with creating new room. Error code: " + returnCode + " Error message: " + message);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Can't join room. Error code: " + returnCode + " Error message: " + message);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Max number of players in " + PhotonNetwork.CurrentRoom.Name + " is " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    public void GoToGameScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}

