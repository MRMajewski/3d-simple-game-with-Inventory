using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> items = new List<ItemData>();

    [SerializeField]
    private InventoryUI inventoryUIManager;

    async void Start()
    {
        await LoadItems();
    }

    private async Task LoadItems()
    {
        GameServerMock server = new GameServerMock();
        string json = await server.GetItemsAsync();

        JObject rootObject = JObject.Parse(json);
        JArray itemsArray = (JArray)rootObject["Items"];

        foreach (var item in itemsArray)
        {
            ItemData newItem = item.ToObject<ItemData>();
            items.Add(newItem);      
        }
        inventoryUIManager.SetupInventoryUI(items);
    }
}
