using System.Collections;
using System.Collections.Generic;
using ButtonAttribute;
using DG.Tweening;
using UnityEngine;

public class WindowPanel : MonoBehaviour
{
    [SerializeField] private Vector2 _openPosition;
    [SerializeField] private Vector2 _closePosition;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease _ease;
    
    private RectTransform _rectTrm;
    private bool _isOpen = false;
    
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _rectTrm.anchoredPosition = _closePosition;
        
    }
    
    [InspectorButton("Open", 10, true, "Open Window")]
    public void Open()
    {
        if (_isOpen)
            return;
        
        _isOpen = true;
        _rectTrm.DOAnchorPos(_openPosition, _duration).SetEase(_ease);
    }
    
    [InspectorButton("Close", 10, true, "Close Window")]
    public void Close()
    {
        if (!_isOpen)
            return;
        
        _isOpen = false;
        _rectTrm.DOAnchorPos(_closePosition, _duration).SetEase(_ease);
    }
}

