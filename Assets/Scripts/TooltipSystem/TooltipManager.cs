using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField]
    private TooltipUI currentTooltip;

    public TooltipUI CurrentTooltip
    {
        get => currentTooltip; set => currentTooltip = value;
    }

    #region Variables

    [SerializeField]
    protected RectTransform tooltipsContainer;
    public RectTransform TooltipsContainer { get => tooltipsContainer; set => tooltipsContainer = value; }

    private RectTransform inspectedRectTransform;
    public RectTransform InspectedRectTransform { get => inspectedRectTransform; set => inspectedRectTransform = value; }

    [Space]
    [Header("Tween related refs")]
    private Sequence showSequence;
    private Sequence hideSequence;

    [SerializeField]
    private float tweenSpeed;
    [SerializeField]
    private float tweenDelay;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    internal void ShowTooltip()
    {
        RepositionToolTip();
        OpenTooltipUI();
    }

    public void OpenTooltipUI()
    {
        if (hideSequence != null) hideSequence.Kill();

        currentTooltip.CanvasGroup.alpha = 0;

        showSequence = DOTween.Sequence();
        showSequence
        .Append(currentTooltip.CanvasGroup.DOFade(1f, tweenSpeed))
        .SetUpdate(true);
    }
    public void HideTooltip()
    {
        CloseTooltipUI();
    }

    public void CloseTooltipUI()
    {
        if (showSequence != null) showSequence.Kill();

        hideSequence = DOTween.Sequence();
        hideSequence
        .Append(currentTooltip.CanvasGroup.DOFade(0f, tweenSpeed))
        .AppendInterval(tweenSpeed).SetUpdate(true);
    }

    public void RepositionToolTip()
    {
        currentTooltip.transform.localScale = Vector3.one;
        currentTooltip.transform.localRotation = Quaternion.identity;

        RectTransform rectTransform = currentTooltip.GetComponent<RectTransform>();

        Vector3 inspectedLocalPos = tooltipsContainer.InverseTransformPoint(inspectedRectTransform.position);

        float horizontalDirection = inspectedLocalPos.x >= 0 ? -1 : 1;
        float verticalDirection = inspectedLocalPos.y >= 0 ? -1 : 1;  

        float offsetX = (inspectedRectTransform.rect.width * 0.5f + rectTransform.rect.width * 0.5f) * horizontalDirection;
        float offsetY = (inspectedRectTransform.rect.height * 0.5f + rectTransform.rect.height * 0.5f) * verticalDirection;

        Vector3 basePosition = inspectedRectTransform.position + new Vector3(offsetX, offsetY, 0);
        rectTransform.position = basePosition;

        rectTransform.anchoredPosition = AdjustToStayOnScreen(rectTransform, tooltipsContainer, rectTransform.anchoredPosition);

        Vector2 AdjustToStayOnScreen(RectTransform currentRect, RectTransform canvasRect, Vector2 newPos)
        {
            float minX = (canvasRect.rect.width * -0.5f) + (currentRect.rect.width * 0.5f);
            float maxX = (canvasRect.rect.width * 0.5f) - (currentRect.rect.width * 0.5f);
            float minY = (canvasRect.rect.height * -0.5f) + (currentRect.rect.height * 0.5f);
            float maxY = (canvasRect.rect.height * 0.5f) - (currentRect.rect.height * 0.5f);

            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            return newPos;
        }
    }
}
