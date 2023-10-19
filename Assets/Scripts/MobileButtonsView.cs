using UnityEngine;
using UnityEngine.UI;

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
        if (playerInventory.MainWeaponItemStack.Item != null)
            rightHandWeaponImage.sprite = playerInventory.MainWeaponItemStack.Item.Icon;
        else
            rightHandWeaponImage.sprite = fistSprite;

        if (playerInventory.SecondaryWeaponItemStack.Item != null)
            leftHandWeaponImage.sprite = playerInventory.SecondaryWeaponItemStack.Item.Icon;
        else
            leftHandWeaponImage.sprite = fistSprite;
    }
}
