using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtonsManager : MonoBehaviour
{
    public ButtonsManager buttonsManager;
    public void MobileAttackButton()
    {
        buttonsManager.AttackButton();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
