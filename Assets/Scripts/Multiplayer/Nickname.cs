using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Nickname : MonoBehaviour
{
    [SerializeField] GameObject nickNameViewPrefab;
    [SerializeField] Transform thisCreature;
    [SerializeField] PhotonView thisCreaturePhotonView;
    [SerializeField] Vector3 offset;
    [SerializeField] Health characterHealth;

    NicknameView nicknameView;
    GameObject characterNickname;
    Camera camera;
    void Start()
    {
        characterHealth.dieEvent += () => Destroy(gameObject);
        camera = Links.instance.mainCamera.GetComponent<Camera>();
        characterNickname = Instantiate(nickNameViewPrefab, Links.instance.healthBarsContainer);
        nicknameView = characterNickname.GetComponent<NicknameView>();
    }
    void FixedUpdate()
    {
        characterNickname.transform.position = camera.WorldToScreenPoint(thisCreature.position) + offset;
        nicknameView.SetNickname(thisCreaturePhotonView.Owner.NickName);
    }
    private void OnDestroy()
    {
        Destroy(characterNickname);
    }

}
