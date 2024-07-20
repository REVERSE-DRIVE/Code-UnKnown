using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DarkPanel : MonoBehaviour, IWindowPanel
{
    private CanvasGroup _canvasGroup;
    private bool _isActive;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        if (_isActive) return;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, 0.2f).SetUpdate(true).OnComplete(() => _isActive = true);
    }

    public void Close()
    {
        if (!_isActive) return;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, 0.2f).SetUpdate(true).OnComplete(() => _isActive = false);
    }
}
