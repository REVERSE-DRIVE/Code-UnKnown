using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FixEditorPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Image _gaugeSubFill;
    [SerializeField] private float _openDuration = 0.1f;
    [SerializeField] private float _gaugeFillDuration = 0.2f;
    private Vector3 _defaultScale = Vector3.one;
    private RectTransform _rectTrm;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        SetCanvas(true);
        Sequence seq = DOTween.Sequence();
        _rectTrm.localScale = Vector3.zero;
        ResetGauge();
        seq.SetUpdate(true);
        seq.Append(_rectTrm.DOScaleX(_defaultScale.x, _openDuration));
        seq.Append(_rectTrm.DOScaleY(_defaultScale.y, _openDuration));
        seq.AppendCallback(RefreshGauge);
    }

    private void RefreshGauge()
    {
        ResetGauge();
        _gaugeSubFill.DOFillAmount(0.8f, _gaugeFillDuration);
        _gaugeFill.DOFillAmount(0.5f, _gaugeFillDuration);
    }

    private void ResetGauge()
    {
        _gaugeSubFill.fillAmount = 0f;
        _gaugeFill.fillAmount = 0f;
    }
    
    public void Close()
    {
        SetCanvas(false);
    }

    private void SetCanvas(bool value)
    {
        _canvasGroup.alpha = value ? 1f : 0f;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
