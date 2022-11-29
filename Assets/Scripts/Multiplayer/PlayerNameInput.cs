using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class PlayerNameInput : MonoBehaviour
{
    string playerNamePrefKey = "PlayerName";
    [SerializeField] TMP_InputField playerNameInputField;
    void Start()
    {
        if (PlayerPrefs.HasKey(playerNamePrefKey) == false)
        {
            string defaultName = "Player" + Random.Range(0, 99999);
            PlayerPrefs.SetString(playerNamePrefKey, defaultName);
            playerNameInputField.text = defaultName;
            PhotonNetwork.NickName = defaultName;
        }
        else
        {
            playerNameInputField.text = PlayerPrefs.GetString(playerNamePrefKey);
            PhotonNetwork.NickName = PlayerPrefs.GetString(playerNamePrefKey);
        }
    }
    public void SetPlayerName()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            playerNameInputField.text = PlayerPrefs.GetString(playerNamePrefKey);
            return;
        }
        PlayerPrefs.SetString(playerNamePrefKey, playerNameInputField.text);
        PhotonNetwork.NickName = playerNameInputField.text;
    }
}

