using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemCost;

    public void SetItemData(Item item)
    {
        itemName.text = item.Info.Name;
        itemDescription.text = item.Info.Description;
        itemCost.text = item.Info.Cost.ToString();
    }
}
