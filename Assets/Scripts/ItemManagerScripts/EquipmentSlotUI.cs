using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using DG.Tweening; 

public class EquipmentSlotUI : ItemBaseSlotUI
{
    public string Category;
    public ItemSlotUI EquippedItem { get; private set; }
    public bool IsEmpty => EquippedItem == null;

    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private float animationDuration = 0.2f; 
    [SerializeField]
    private float scaleMultiplier = 1.1f; 

    private void Awake()
    {
        itemButton.onClick.AddListener(UnequipItem);
    }

    public void EquipItem(ItemSlotUI item)
    {
        this.ItemData = item.ItemData;
        EquippedItem = item;
        itemIcon.sprite = item.itemIcon.sprite;

        SetRarityColor(ItemData.Rarity);
        tooltipTrigger.TooltipText = GetItemDescription();
        item.gameObject.SetActive(false);

        AnimateEquipItem();

        CharacterPreview.Instance.UpdateCharacterLook(Category, ItemData.Rarity);
        EquipmentStatsManager.Instance.UpdateStats();
    }

    public void UnequipItem()
    {
        if (IsEmpty) return;

        EquippedItem.gameObject.SetActive(true);
        EquippedItem = null;
        itemIcon.sprite = emptySprite;
        SetRarityColor(0);

        CharacterPreview.Instance.UpdateCharacterLook(Category);
        EquipmentStatsManager.Instance.UpdateStats();
    }

    private void AnimateEquipItem()
    {
        this.transform.DOScale(scaleMultiplier, animationDuration).SetUpdate(true)
            .OnComplete(() =>
            {
                this.transform.DOScale(1f, animationDuration).SetEase(Ease.InBack).SetUpdate(true);
            });
    }
}
