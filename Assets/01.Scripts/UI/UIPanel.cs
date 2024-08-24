using System;
using DG.Tweening;
using UnityEngine;

public class UIPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private float _activeDuration = 1f;
    [SerializeField] private bool _useUnscaleedTime;
    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {        
        SetCanvasGroup(true);
    }

    public virtual void Close()
    {
        SetCanvasGroup(false);

    }
    public void SetCanvasGroup(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration).SetUpdate(_useUnscaleedTime);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}