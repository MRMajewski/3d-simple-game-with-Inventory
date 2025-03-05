using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipTrigger : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private string tooltipText;
    public string TooltipText
    {
        get => tooltipText;
        set => tooltipText = value;
    }
    public void OnSelect(BaseEventData eventData)
    {
        HandleTooltipEnter();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleTooltipEnter();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        HandleTooltipEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HandleTooltipExit();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HandleTooltipExit();
    }

    private void HandleTooltipEnter()
    {
        TooltipManager.Instance.InspectedRectTransform = GetComponent<RectTransform>();

        TooltipManager.Instance.CurrentTooltip.SetTooltipText(tooltipText);

        TooltipManager.Instance.CurrentTooltip.ResizeTooltip();

        TooltipManager.Instance.ShowTooltip();
    }

    private void HandleTooltipExit()
    {
        TooltipManager.Instance.HideTooltip();

    }
}
