using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AlertPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _contentTmp;
    [SerializeField] private float _moveLength = 200f;
    private float _duration = 0;
    private RectTransform _rectTrm;
    private Vector2 _targetPosition;
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = transform as RectTransform;
        
    }

    public void Initialize(string content, float duration)
    {
        _canvasGroup.alpha = 1f;
        _contentTmp.text = content;
        _duration = duration;

        _targetPosition = _rectTrm.anchoredPosition + new Vector2(0, _moveLength);
        MoveFadeOut();
    }

    private void MoveFadeOut()
    {
        _rectTrm.DOAnchorPos(_targetPosition, _duration);
        _canvasGroup.DOFade(0, _duration);
    }
    
    private void PlayCoroutine()
    {
        
    }
}