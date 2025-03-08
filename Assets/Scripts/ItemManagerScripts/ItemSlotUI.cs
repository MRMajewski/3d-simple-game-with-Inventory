using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : ItemBaseSlotUI
{
    public override void Setup(ItemData itemData)
    {
        base.Setup(itemData);
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => TryEquipItem());
    }

    private void TryEquipItem()
    {
        foreach (var slot in InventoryUI.Instance.GetEquipmentSlots()) 
        {
            if (slot.Category == ItemData.Category)
            {
                if (!slot.IsEmpty)
                    slot.UnequipItem();

                slot.EquipItem(this);
                return;
            }
        }
        Debug.Log("No available equipment slot for this item!");
    }
}
