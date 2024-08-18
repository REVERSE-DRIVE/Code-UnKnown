using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour, IWindowPanel
{
    [Header("PanelMove Setting")]
    [SerializeField] private float _defaultYDelta;
    [SerializeField] private float _activeYDeltas;
    [SerializeField] private float _moveDuration;
    
    [Header("Coloring Setting")]
    [SerializeField] private Image _coloringBarPanel;
    [SerializeField] private Image _coloringSubPanel;
    [SerializeField] private Image _coloringPanel;
    [SerializeField] private float _coloringDuration;
    
    [Header("TextGlitch Setting")]
    [SerializeField] private RectTransform _redTextPanel;
    [SerializeField] private RectTransform _cyanTextPanel;
    private RectTransform _rectTrm;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public void Open()
    {
        
        StartCoroutine(OpenCoroutine());
    }

    public void Close()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator OpenCoroutine()
    {
        float currentTime = 0;
        while (currentTime < _coloringDuration)
        {
            currentTime += Time.deltaTime;
            SetColorPanels(Color.Lerp(Color.white, Color.red, currentTime / _coloringDuration));
            yield return null;
        }

    }

    private void SetColorPanels(Color color)
    {
        _coloringBarPanel.color = color;
        _coloringSubPanel.color = color;
        _coloringPanel.color = color;

    }
    
    
}