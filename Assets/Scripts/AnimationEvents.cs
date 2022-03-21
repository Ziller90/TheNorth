using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public MeleeAttack meleeAttack;
    public void AttackFinished()
    {
        meleeAttack.StopAttack();
    }

    public void DisableRotation()
    {
        meleeAttack.DisableRotation();
    }

    public void AllowRotation()
    {
        meleeAttack.AllowRotation();
    }
}
