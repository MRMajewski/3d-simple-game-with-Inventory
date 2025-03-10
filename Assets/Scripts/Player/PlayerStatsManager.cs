using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
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

    private void Start()
    {
        totalDamage = 1;
        totalHealthPoints = 1;
        totalDefense = 1;
        totalLifeSteal = 1;
        totalCriticalStrikeChance = 1;
        totalAttackSpeed = 1;
        totalMovementSpeed = 1;
        totalLuck = 1;
    }


    public void UpdatePlayerStats()
    {
        totalDamage = EquipmentStatsManager.Instance.TotalDamage;
        totalHealthPoints = EquipmentStatsManager.Instance.TotalHealthPoints;
        totalDefense = EquipmentStatsManager.Instance.TotalDefense;
        totalLifeSteal = EquipmentStatsManager.Instance.TotalLifeSteal;
        totalCriticalStrikeChance = EquipmentStatsManager.Instance.TotalCriticalStrikeChance;
        totalAttackSpeed = EquipmentStatsManager.Instance.TotalAttackSpeed;
        totalMovementSpeed = EquipmentStatsManager.Instance.TotalMovementSpeed;
        totalLuck = EquipmentStatsManager.Instance.TotalLuck;
    }

    internal void TakeDamage(int damage)
    {
      //  throw new NotImplementedException();
    }
}
