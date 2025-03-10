using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; } 

    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private List<EquipmentSlotUI> equipmentSlots;

    [SerializeField] private Camera previewCamera;

    private List<ItemSlotUI> itemUIList = new List<ItemSlotUI>();

    [SerializeField] private GameObject inventoryPanel; // Panel ekwipunku

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of InventoryUI detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    [ContextMenu("ToggleInventory")]
    public void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);
        previewCamera.gameObject.SetActive(isActive);
    }

    public void SetupInventoryUI(List<ItemData> items)
    {
        ClearInventoryUI();

        foreach (ItemData item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(ItemData itemData)
    {
        GameObject newItem = Instantiate(itemUIPrefab, itemsParent);
        ItemSlotUI itemUI = newItem.GetComponent<ItemSlotUI>();
        itemUI.Setup(itemData);
        itemUIList.Add(itemUI);
    }

    private void ClearInventoryUI()
    {
        foreach (var itemUI in itemUIList)
        {
            Destroy(itemUI.gameObject);
        }
        itemUIList.Clear();
    }

    public List<EquipmentSlotUI> GetEquipmentSlots() => equipmentSlots; 
}
