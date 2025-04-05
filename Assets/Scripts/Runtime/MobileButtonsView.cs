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
    [SerializeField] GameObject quickSlots;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject takeButton;

    HumanoidInventoryContainer playerInventory;

    void Awake()
    {
        playerInventory = Game.GameSceneInitializer.Player.GetComponentInChildren<HumanoidInventoryContainer>();
        SetUpQuickSlotsButtons();
    }

    void SetUpQuickSlotsButtons()
    {
        for (int i = 0; i < playerInventory.QuickAccessSlots.Slots.Count; i++)
        {
            quickSlotViews[i].SetSlot(playerInventory.QuickAccessSlots.Slots[i]);
            quickSlotViews[i].IsInteratable = false;
        }
    }

    void Update()
    {
        if (!playerInventory.MainWeaponSlot.IsEmpty)
            rightHandWeaponImage.sprite = playerInventory.MainWeaponSlot.ItemStack.Item.Icon;
        else
            rightHandWeaponImage.sprite = fistSprite;

        if (!playerInventory.SecondaryWeaponSlot.IsEmpty)
            leftHandWeaponImage.sprite = playerInventory.SecondaryWeaponSlot.ItemStack.Item.Icon;
        else
            leftHandWeaponImage.sprite = fistSprite;
    }

    public void UseAcessButtonItem(int index)
    {
        if (playerInventory.QuickAccessSlots.Slots[index].ItemStack != null)
            playerInventory.UseUsableItem(playerInventory.QuickAccessSlots.Slots[index].ItemStack);
    }
}
