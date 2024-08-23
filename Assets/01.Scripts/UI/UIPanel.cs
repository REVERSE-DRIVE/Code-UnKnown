using System;
using DG.Tweening;
using UnityEngine;

public class UIPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private float _activeDuration = 1f;

    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {        
        SetCanvasGroup(true);
    }

    public void Close()
    {
        SetCanvasGroup(false);

    }
    public void SetCanvasGroup(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}