using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HitBox : MonoBehaviourPun
{
    [SerializeField] Health health;
    [SerializeField] float hitBoxDamageModificator;
    [SerializeField] HumanoidBattleSystem battleSystem;
    [SerializeField] GameObject DebugPoint;

    public Transform thisCreature;
    public void HitBoxGetDamage(float damage, Vector3 hitPoint)
    {
        if (battleSystem.ShieldRaised)
        {
            if (Vector3.Angle(-battleSystem.GetHitVector(hitPoint), thisCreature.forward) > battleSystem.ShieldProtectionAngle / 2)
            {
                if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch && PhotonNetwork.IsMasterClient)
                {
                    health.GetComponent<PhotonView>().RPC("GetDamage", RpcTarget.All, damage);
                }
                else if (GameSceneLauncher.LocationToLoadGameType == GameType.Singleplayer)
                {
                    health.GetDamage(damage);
                }
            }
        }
        else
        {
            if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch && PhotonNetwork.IsMasterClient)
            {
                health.GetComponent<PhotonView>().RPC("GetDamage", RpcTarget.All, damage);
            }
            else if (GameSceneLauncher.LocationToLoadGameType == GameType.Singleplayer)
            {
                health.GetDamage(damage);
            }
        }
    }
}
