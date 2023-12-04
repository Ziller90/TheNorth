using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] TMP_Text moneyCountText;
    HumanoidInventoryContainer humanoidInventory;
    public void SetHumanoidInventory(HumanoidInventoryContainer inventory)
    {
        humanoidInventory = inventory;
        UpdateMoneyView();
    }
    void OnEnable()
    {
        if (humanoidInventory)
            humanoidInventory.moneyAmountUpdated += UpdateMoneyView;
    }
    void OnDisable()
    {
        if (humanoidInventory)
            humanoidInventory.moneyAmountUpdated -= UpdateMoneyView;
    }
    void UpdateMoneyView()
    {
        moneyCountText.text = humanoidInventory.MoneyAmount.ToString();
    }
}
