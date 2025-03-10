using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPreview : MonoBehaviour
{
    public static CharacterPreview Instance { get; private set; }

    [SerializeField]
    private CharacterModelEquipmentChanger playerPreviewModel;

    [SerializeField]
    private CharacterModelEquipmentChanger playerGameModel;
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

    public void UpdateCharacterLook(string category, int index=-1)
    {
        playerPreviewModel.ChangeEquipment(category, index);
        playerGameModel.ChangeEquipment(category, index);
    }
}
