using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public HumanoidBattleSystem battleSystem;
    public void AttackFinished()
    {
        battleSystem.StopAttack();
    }

    public void DisableRotation()
    {
        battleSystem.DisableRotation();
    }

    public void AllowRotation()
    {
        battleSystem.AllowRotation();
    }
}
