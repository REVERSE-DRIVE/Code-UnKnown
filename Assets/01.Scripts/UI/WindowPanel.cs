using DG.Tweening;
using UnityEngine;

public class WindowPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Vector2 _openPosition;
    [SerializeField] private Vector2 _closePosition;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease _ease;
    
    protected RectTransform _rectTrm;
    protected bool _isOpen = false;
    
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _rectTrm.anchoredPosition = _closePosition;
        
    }
    
    public virtual void Open()
    {
        if (_isOpen)
            return;
        
        _isOpen = true;
        _rectTrm.DOAnchorPos(_openPosition, _duration).SetEase(_ease);
    }
    
    public virtual void Close()
    {
        if (!_isOpen)
            return;
        
        _isOpen = false;
        _rectTrm.DOAnchorPos(_closePosition, _duration).SetEase(_ease);
    }
}

