using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    public bool controlPlayer;

    public void Update()
    {

    }
    public void Start()
    {
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(1, 3), 2, Random.Range(1, 3)), Quaternion.identity, 0);
        Debug.Log("Your ID in room is" + PhotonNetwork.LocalPlayer.ActorNumber);
    }
    public void LeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("LobbyScene");
        }
        else
        {
            SceneManager.LoadScene("LobbyScene");
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
