using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ItemBaseSlotUI : MonoBehaviour
{
    public ItemData ItemData { get; protected set; }
    public Image itemIcon;
    public Image background;
    public Button itemButton;
    [SerializeField]
    protected TooltipTrigger tooltipTrigger;

    [Serializable]
    public struct CategoryIcon
    {
        public string categoryName;
        public Sprite icon;
    }

    [SerializeField] protected List<CategoryIcon> categoryIcons;
    [SerializeField] protected List<Color> rarityColors;

    public virtual void Setup(ItemData itemData)
    {
        ItemData = itemData;
        itemIcon.sprite = GetItemIcon(ItemData.Category);
        SetRarityColor(ItemData.Rarity);
        tooltipTrigger.TooltipText = GetItemDescription();
    }

    protected Sprite GetItemIcon(string category)
    {
        foreach (var entry in categoryIcons)
        {
            if (entry.categoryName == category)
                return entry.icon;
        }
        Debug.LogWarning($"No icon for category: {category}");
        return null;
    }

    protected void SetRarityColor(int rarity)
    {
        if (rarity >= 0 && rarity < rarityColors.Count)
        {
            background.color = rarityColors[rarity];
        }
        else
        {
            Debug.LogWarning($"Invalid rarity: {rarity}, setting default color.");
            background.color = Color.white;
        }
    }

    protected string GetItemDescription()
    {
        return $"<b><size=120%>{ItemData.Name}\n</size></b>" +
               $"Category: {ItemData.Category}\n" +
               $"Rarity: {ItemData.Rarity}\n" +
               $"Damage: {ItemData.Damage}\n" +
               $"Health Points: {ItemData.HealthPoints}\n" +
               $"Defense: {ItemData.Defense}\n" +
               $"Life Steal: {ItemData.LifeSteal:F2}%\n" +
               $"Critical Strike: {ItemData.CriticalStrikeChance:F2}%\n" +
               $"Attack Speed: {ItemData.AttackSpeed:F2}\n" +
               $"Movement Speed: {ItemData.MovementSpeed:F2}\n" +
               $"Luck: {ItemData.Luck:F2}";
    }
}