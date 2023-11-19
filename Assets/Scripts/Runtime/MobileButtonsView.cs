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
    [SerializeField] Image[] accessButtonIcons;
    [SerializeField] TMP_Text[] acessButtonsItemNumber;

    private void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventory>();
    }

    void Update()
    {
        if (!playerInventory.MainWeaponSlot.isEmpty)
            rightHandWeaponImage.sprite = playerInventory.MainWeaponSlot.ItemStack.Item.Icon;
        else
            rightHandWeaponImage.sprite = fistSprite;

        if (!playerInventory.SecondaryWeaponSlot.isEmpty)
            leftHandWeaponImage.sprite = playerInventory.SecondaryWeaponSlot.ItemStack.Item.Icon;
        else
            leftHandWeaponImage.sprite = fistSprite;

        for (int i = 0; i < accessButtonIcons.Length; i++)
        {
            if (!playerInventory.QuickAccessSlots[i].isEmpty)
                accessButtonIcons[i].sprite = playerInventory.QuickAccessSlots[i].ItemStack.Item.Icon;
            else
                accessButtonIcons[i].sprite = noneSprite;

            var itemsNumber = playerInventory.QuickAccessSlots[i].ItemStack.ItemsNumber;
            acessButtonsItemNumber[i].text = itemsNumber == 0 || itemsNumber == 1 ? "" : playerInventory.QuickAccessSlots[i].ItemStack.ItemsNumber.ToString();
        }
    }

    public void UseAcessButtonItem(int index)
    {
        playerInventory.UseItem(playerInventory.QuickAccessSlots[index].ItemStack);
    }
}
