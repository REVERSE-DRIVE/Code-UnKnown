using System;
using System.Collections;
using DG.Tweening;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _mainText;

    private RectTransform _rectTrm;
    private TextMeshProUGUI _redText;
    private TextMeshProUGUI _cyanText;

    protected virtual void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
        _redText = _redTextPanel.GetComponent<TextMeshProUGUI>();
        _cyanText = _cyanTextPanel.GetComponent<TextMeshProUGUI>();
    }

    [ContextMenu("Open")]
    public override void Open()
    {
        SetText("구역 점령");
        SetCanvasGroup(true);
        SetColorPanels(_defualtColor);

        _rectTrm.DOAnchorPosY(_activeYDeltas, _moveDuration).OnComplete(() => StartCoroutine(OpenCoroutine()));
        
    }

    public void Fail() {
        SetText("구역 실패");
        SetCanvasGroup(true);
        SetColorPanels(_defualtColor);

        _rectTrm.DOAnchorPosY(_activeYDeltas, _moveDuration).OnComplete(() => StartCoroutine(OpenCoroutine(false)));
    }

    public override void Close()
    {
        _rectTrm.DOAnchorPosY(_defaultYDelta, _moveDuration);
    }

    private IEnumerator OpenCoroutine(bool colorAnim = true)
    {
        float currentTime = 0;
        while (currentTime < _coloringDuration && colorAnim)
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

    private void SetText(string text) {
        _redText.text = text;
        _cyanText.text = text;
        _mainText.text = text;
    }
    
    
}