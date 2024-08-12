using System;
using UnityEngine;

public class CreditPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private RectTransform _credirHandle;
    [SerializeField] private float _startYDelta;
    [SerializeField] private float _endYDelta;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        
    }

    public void Close()
    {
        
    }
    
}
