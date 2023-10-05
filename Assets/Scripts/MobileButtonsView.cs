using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MobileButtonsView : MonoBehaviour
{
    [SerializeField] Image rightHandWeaponImage;
    [SerializeField] Image leftHandWeaponImage;
    [SerializeField] Sprite fistSprite;

    HumanoidInventory playerInventory;

    private void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
    }

    void Update()
    {
        if (playerInventory.RightHandItemStack.Item != null)
            rightHandWeaponImage.sprite = playerInventory.RightHandItemStack.Item.Icon;
        else
            rightHandWeaponImage.sprite = fistSprite;

        //if (playerinventory.lefthanditemstack != null)
        //    lefthandweaponimage.sprite = playerinventory.lefthanditemstack.item.icon;
        //else
        //    lefthandweaponimage.sprite = fistsprite;
    }
}
