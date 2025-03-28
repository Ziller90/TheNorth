using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button quickMatchButton;
    [SerializeField] private TMP_Text buttonText;         

    private bool isSearching = false;

    public void OnQuickMatchButtonClicked()
    {
        if (!isSearching)
            StartSearching();
        else
            CancelSearching();
    }

    private void StartSearching()
    {
        isSearching = true;
        buttonText.text = "Searching for battle...";

        if (PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.JoinRandomOrCreateRoom(null, 8, MatchmakingMode.FillRoom);
    }

    private void CancelSearching()
    {
        isSearching = false;
        buttonText.text = "Fight!";
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        if (isSearching)
            PhotonNetwork.JoinRandomOrCreateRoom(null, 8, MatchmakingMode.FillRoom);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (isSearching)
            PhotonNetwork.JoinRandomOrCreateRoom(null, 8, MatchmakingMode.FillRoom);
    }

    public override void OnJoinedRoom()
    {
        isSearching = false;

        Debug.Log("Player joined room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Max number of players in " + PhotonNetwork.CurrentRoom.Name + " is " + PhotonNetwork.CurrentRoom.MaxPlayers);
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("GlobalMapScene");
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
}
