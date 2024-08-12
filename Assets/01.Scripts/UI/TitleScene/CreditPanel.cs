using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CreditPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Button _skipBtn;
    [SerializeField] private RectTransform _credirHandle;
    [SerializeField] private float _startYDelta;
    [SerializeField] private float _endYDelta;
    [SerializeField] private float _duration;
    private CanvasGroup _canvasGroup;
    private Tween _tween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        _skipBtn.onClick.AddListener(HandleSkip);
    }

    private void HandleSkip()
    {
        if(_tween != null)
            _tween.Kill();
        
        Close();
    }

    [ContextMenu("ming")]
    public void Open()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        Play();
    }

    public void Close()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    
    public void Play()
    {
        _credirHandle.anchoredPosition = new Vector2(0, _startYDelta);
        _tween = _credirHandle.DOAnchorPosY(_endYDelta, _duration).OnComplete(Close);
    }
    
}
