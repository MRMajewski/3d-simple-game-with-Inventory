using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject itemUIPrefab;

    private List<ItemUI> itemUIList = new List<ItemUI>();

    public void SetupInventoryUI(List<ItemData> items)
    {
        ClearInventoryUI();

        foreach (ItemData item in items)
        {
            AddItem(item);
        }

        void AddItem(ItemData itemData)
        {
            GameObject newItem = Instantiate(itemUIPrefab, itemsParent);
            ItemUI itemUI = newItem.GetComponent<ItemUI>();
            itemUI.Setup(itemData);
            itemUIList.Add(itemUI);
        }

        void ClearInventoryUI()
        {
            foreach (var itemUI in itemUIList)
            {
                Destroy(itemUI.gameObject);
            }
            itemUIList.Clear();
        }

    }
}
