using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MobileButtonsView : MonoBehaviour
{
    [SerializeField] Image rightHandWeaponImage;
    [SerializeField] Image leftHandWeaponImage;
    [SerializeField] Sprite fistSprite;
    [SerializeField] Sprite noneSprite;
    [SerializeField] SlotView[] quickSlotViews;

    HumanoidInventoryContainer playerInventory;

    private void Awake()
    {
        playerInventory = Links.instance.playerCharacter.GetComponentInChildren<HumanoidInventoryContainer>();
    }

    void Start()
    {
        SetUpQuickSlotsButtons();
    }

    void SetUpQuickSlotsButtons()
    {
        for (int i = 0; i < playerInventory.QuickAccessSlots.Slots.Length; i++)
            quickSlotViews[i].SetSlot(playerInventory.QuickAccessSlots.Slots[i], null);
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
    }

    public void UseAcessButtonItem(int index)
    {
        playerInventory.UseUsableItem(playerInventory.QuickAccessSlots.Slots[index].ItemStack);
    }
}
