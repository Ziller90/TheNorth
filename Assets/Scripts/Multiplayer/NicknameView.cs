using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NicknameView : MonoBehaviour
{
    string nicknameString;
    [SerializeField] TMP_Text nicknameText;
    public void SetNickname(string nickname)
    {
        nicknameString = nickname;
    }
    void Update()
    {
        nicknameText.text = nicknameString;
    }
}
