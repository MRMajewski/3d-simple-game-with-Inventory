using DG.Tweening;
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
    private Sequence loadingSequence;

    [SerializeField] private GameObject inventoryPanel; 

    [SerializeField] private CanvasGroup onLoadTextCanvasGroup;

    public List<EquipmentSlotUI> EquipmentSlots { get => equipmentSlots; }

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

    private void Start()
    {
        StartLoadingAnimation();
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
        StopLoadingAnimation();
        onLoadTextCanvasGroup.gameObject.SetActive(false);
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

    private void StartLoadingAnimation()
    {
        if (loadingSequence != null && loadingSequence.IsActive())
        {
            loadingSequence.Kill(); 
        }

        loadingSequence = DOTween.Sequence()
            .Append(onLoadTextCanvasGroup.DOFade(0.3f, 0.8f))
            .Append(onLoadTextCanvasGroup.DOFade(1f, 0.8f))
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
    }

    private void StopLoadingAnimation()
    {
        if (loadingSequence != null && loadingSequence.IsActive())
        {
            loadingSequence.Kill(); 
            loadingSequence = null;
        }
    }
}
