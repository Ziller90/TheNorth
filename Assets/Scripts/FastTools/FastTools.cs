using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class FastTools : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Cheats/Kill Player Character")]
    public static void KillPlayerCharacter()
    {
        Links.instance.playerCharacter.GetComponentInChildren<Health>().GetDamage(100000000);
    }
#endif
}
