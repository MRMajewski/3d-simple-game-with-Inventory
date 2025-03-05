using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public ItemData ItemData { get; private set; } 

    public Image itemIcon; 
    public Image background; 
    public TextMeshProUGUI itemNameText;

    [SerializeField]
    private TooltipTrigger tooltipTrigger;

    [Serializable]
    public struct CategoryIcon
    {
        public string categoryName;
        public Sprite icon;
    }

    [SerializeField] private List<CategoryIcon> categoryIcons; 
    [SerializeField] private List<Color> rarityColors; 

    public void Setup(ItemData itemData)
    {
        ItemData = itemData; 

        Debug.Log($"Ustawiam ItemUI: {ItemData.Name}, Kategoria: {ItemData.Category}, Rarity: {ItemData.Rarity}");

        itemNameText.text = ItemData.Name;
        itemIcon.sprite = GetItemIcon(ItemData.Category);
        SetRarityColor(ItemData.Rarity);
        tooltipTrigger.TooltipText=GetItemDescription();

        Sprite GetItemIcon(string category)
        {
            foreach (var entry in categoryIcons)
            {
                if (entry.categoryName == category)
                    return entry.icon;
            }
            Debug.LogWarning($"Brak ikony dla kategorii: {category}");
            return null; 
        }

        void SetRarityColor(int rarity)
        {
            if (rarity >= 0 && rarity < rarityColors.Count)
            {
                background.color = rarityColors[rarity];
            }
            else
            {
                Debug.LogWarning($"Nieprawid³owy rarity: {rarity}, ustawiam domyœlny kolor.");
                background.color = Color.white;
            }
        }

        string GetItemDescription()
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
}
