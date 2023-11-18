using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MobileButtonsView : MonoBehaviour
{
    [SerializeField] Image rightHandWeaponImage;
    [SerializeField] Image leftHandWeaponImage;
    [SerializeField] Sprite fistSprite;
    [SerializeField] Sprite noneSprite;

    HumanoidInventory playerInventory;
    [SerializeField] Image[] acessButtonIcons;
    [SerializeField] TMP_Text[] acessButtonsItemNumber;

    private void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
    }

    void Update()
    {
        if (playerInventory.MainWeaponItemStack.Item != null)
            rightHandWeaponImage.sprite = playerInventory.MainWeaponItemStack.Item.Icon;
        else
            rightHandWeaponImage.sprite = fistSprite;

        if (playerInventory.SecondaryWeaponItemStack.Item != null)
            leftHandWeaponImage.sprite = playerInventory.SecondaryWeaponItemStack.Item.Icon;
        else
            leftHandWeaponImage.sprite = fistSprite;

        for (int i = 0; i < acessButtonIcons.Length; i++)
        {
            if (playerInventory.QuickAccessItemStacks[i].Item != null)
                acessButtonIcons[i].sprite = playerInventory.QuickAccessItemStacks[i].Item.Icon;
            else
                acessButtonIcons[i].sprite = noneSprite;

            var itemsNumber = playerInventory.QuickAccessItemStacks[i].ItemsNumber;
            acessButtonsItemNumber[i].text = itemsNumber == 0 || itemsNumber == 1 ? "" : playerInventory.QuickAccessItemStacks[i].ItemsNumber.ToString();
        }
    }

    public void UseAcessButtonItem(int index)
    {
        playerInventory.UseItem(playerInventory.QuickAccessItemStacks[index]);
    }
}
