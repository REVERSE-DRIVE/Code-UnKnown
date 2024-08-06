using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaptopPanel : MonoBehaviour, IWindowPanel, IPointerClickHandler
{
    [SerializeField] private float _openDuration, _openScaleX, _openScaleY;
    [SerializeField] private Ease _ease;
    private RectTransform _rectTransform;

    protected void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScaleX(_openScaleX, _openDuration));
        sequence.AppendInterval(0.2f);
        sequence.Append(_rectTransform.DOScaleY(_openScaleY, _openDuration));
        sequence.SetEase(_ease);
        
        sequence.Play();
    }
 
    public void Close()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScaleX(0, _openDuration));
        sequence.AppendInterval(0.2f);
        sequence.Append(_rectTransform.DOScaleY(0, _openDuration));
        sequence.SetEase(_ease);
        
        sequence.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _rectTransform.SetAsLastSibling();
        _rectTransform.localPosition = eventData.position;
    }
}