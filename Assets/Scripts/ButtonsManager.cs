 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public bool isMeleeAttack;
    public bool isDistantAttack;
    public bool isBlock;

    public HumanoidBattleSystem battleSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        battleSystem.isMeleeAttack = isMeleeAttack;
        battleSystem.isBlock = isBlock;
    }
}
