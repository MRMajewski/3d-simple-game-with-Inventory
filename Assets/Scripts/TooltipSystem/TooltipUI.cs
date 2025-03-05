using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class TooltipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tooltipText;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup=>canvasGroup;

    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    public void SetTooltipText(string text)
    {
        tooltipText.text = text;
    }

    public void ResizeTooltip()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }
    IEnumerator ResizeCoroutine()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        yield return wait;

    }
    public void HandleTooltipExit()
    {
        TooltipManager.Instance.HideTooltip();

    }
}
