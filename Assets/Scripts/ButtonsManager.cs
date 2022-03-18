using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public MeleeAttack meleeAttack;
    public void AttackButton() 
    {
        meleeAttack.StartCoroutine("Attack");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
