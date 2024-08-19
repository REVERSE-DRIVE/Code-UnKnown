using System;
using DG.Tweening;
using UnityEngine;

public class EditorPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private float _activeDuration;
    private CanvasGroup _canvasGroup;

    private void Awake()
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

    private void SetCanvasGroup(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
