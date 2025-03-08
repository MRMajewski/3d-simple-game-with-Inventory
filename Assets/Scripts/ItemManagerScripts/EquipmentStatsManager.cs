using UnityEngine;
using TMPro;

public class EquipmentStatsManager : MonoBehaviour
{
    public static EquipmentStatsManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI statsTextDamage;
    [SerializeField] private TextMeshProUGUI statsTextHealth;
    [SerializeField] private TextMeshProUGUI statsTextDefence;
    [SerializeField] private TextMeshProUGUI statsTextLifeSteal;
    [SerializeField] private TextMeshProUGUI statsTextCritical;
    [SerializeField] private TextMeshProUGUI statsTextAttackSpeed;
    [SerializeField] private TextMeshProUGUI statsTextMovementSpeed;
    [SerializeField] private TextMeshProUGUI statsTextLuck;

    // Statystyki sumowane
    private int totalDamage;
    private int totalHealthPoints;
    private int totalDefense;
    private float totalLifeSteal;
    private float totalCriticalStrikeChance;
    private float totalAttackSpeed;
    private float totalMovementSpeed;
    private float totalLuck;

    public int TotalDamage => totalDamage;
    public int TotalHealthPoints => totalHealthPoints;
    public int TotalDefense => totalDefense;
    public float TotalLifeSteal => totalLifeSteal;
    public float TotalCriticalStrikeChance => totalCriticalStrikeChance;
    public float TotalAttackSpeed => totalAttackSpeed;
    public float TotalMovementSpeed => totalMovementSpeed;
    public float TotalLuck => totalLuck;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of EquipmentStatsManager detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        totalDamage = 0;
        totalHealthPoints = 0;
        totalDefense = 0;
        totalLifeSteal = 0f;
        totalCriticalStrikeChance = 0f;
        totalAttackSpeed = 0f;
        totalMovementSpeed = 0f;
        totalLuck = 0f;

        foreach (EquipmentSlotUI slot in InventoryUI.Instance.GetEquipmentSlots())
        {
            if (!slot.IsEmpty)
            {
                ItemData itemData = slot.EquippedItem.ItemData;

                totalDamage += itemData.Damage;
                totalHealthPoints += itemData.HealthPoints;
                totalDefense += itemData.Defense;
                totalLifeSteal += itemData.LifeSteal;
                totalCriticalStrikeChance += itemData.CriticalStrikeChance;
                totalAttackSpeed += itemData.AttackSpeed;
                totalMovementSpeed += itemData.MovementSpeed;
                totalLuck += itemData.Luck;
            }
        }

        DisplayStats();
    }

    private void DisplayStats()
    {
        statsTextDamage.text = $"Damage: {totalDamage}\n";
        statsTextHealth.text = $"Health: {totalHealthPoints}\n";
        statsTextDefence.text = $"Defense: {totalDefense}\n";
        statsTextLifeSteal.text = $"Life Steal: {totalLifeSteal:0.##}%\n";
        statsTextCritical.text = $"Critical Strike Chance: {totalCriticalStrikeChance:0.##}%\n";
        statsTextAttackSpeed.text = $"Attack Speed: {totalAttackSpeed:0.##}\n";
        statsTextMovementSpeed.text = $"Movement Speed: {totalMovementSpeed:0.##}\n";
        statsTextLuck.text = $"Luck: {totalLuck:0.##}\n";
    }
}
