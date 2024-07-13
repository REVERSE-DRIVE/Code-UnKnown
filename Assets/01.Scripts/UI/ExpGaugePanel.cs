using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExpGaugePanel : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Image _subGaugeFill;
    [SerializeField] private float _duration = 0.5f;

    private Sequence _seq;
    
    public void ResetGauge()
    {
        _gaugeFill.fillAmount = 0;
        _subGaugeFill.fillAmount = 0;
    } // 디버그 안해봄
    
    public void Refresh(int current, int max)
    {
        ResetGauge();
        float ratio = (float)current / max;
        
        _seq = DOTween.Sequence();
        _seq.Append(_subGaugeFill.DOFillAmount(ratio, _duration));
        _seq.Append(_gaugeFill.DOFillAmount(ratio, _duration));
    }
}