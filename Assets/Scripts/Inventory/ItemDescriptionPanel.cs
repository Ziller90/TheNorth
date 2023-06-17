using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemCost;

    public void SetItemData(ItemData itemData)
    {
        itemName.text = itemData.Name;
        itemDescription.text = itemData.Description;
        itemCost.text = itemData.Cost.ToString();
    }
}
