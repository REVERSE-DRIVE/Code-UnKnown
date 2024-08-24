using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : UIPanel
{
    [Header("PanelMove Setting")]
    [SerializeField] private float _defaultYDelta;
    [SerializeField] private float _activeYDeltas;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _panelDisableTerm;

    [Header("Coloring Setting")] 
    [SerializeField] private Color _defualtColor;
    [SerializeField] private Color _targetColor;
    [SerializeField] private Image _coloringGradientPanel;
    [SerializeField] private Image _coloringBarPanel;
    [SerializeField] private Image _coloringSubPanel;
    [SerializeField] private Image _coloringPanel;
    [SerializeField] private float _coloringDuration;
    
    [Header("TextGlitch Setting")]
    [SerializeField] private RectTransform _redTextPanel;
    [SerializeField] private RectTransform _cyanTextPanel;
    private RectTransform _rectTrm;

    protected virtual void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }

    [ContextMenu("Open")]
    public void Open()
    {
        SetCanvasGroup(true);
        SetColorPanels(_defualtColor);

        _rectTrm.DOAnchorPosY(_activeYDeltas, _moveDuration).OnComplete(() => StartCoroutine(OpenCoroutine()));
        
    }

    public void Close()
    {
        _rectTrm.DOAnchorPosY(_defaultYDelta, _moveDuration);
    }

    private IEnumerator OpenCoroutine()
    {
        float currentTime = 0;
        while (currentTime < _coloringDuration)
        {
            currentTime += Time.deltaTime;
            SetColorPanels(Color.Lerp(_defualtColor, _targetColor, currentTime / _coloringDuration));
            yield return null;
        }

        yield return new WaitForSeconds(_panelDisableTerm);
        _canvasGroup.DOFade(0f, 0.3f).OnComplete(() => SetCanvasGroup(false));
        Close();   
    }

    private void SetColorPanels(Color color)
    {
        _coloringGradientPanel.color = color;
        _coloringBarPanel.color = color;
        _coloringSubPanel.color = color;
        _coloringPanel.color = color;

    }

   
    
    
}