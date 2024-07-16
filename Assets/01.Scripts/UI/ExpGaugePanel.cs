using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ExpGaugePanel : MonoBehaviour
{
    [Header("Gauge Setting")]
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private float _duration = 0.5f;
    private readonly int _maxLevel = 25;

    [Header("Text Setting")]
    [SerializeField] private TextMeshProUGUI _levelTmp;
    [SerializeField] private TextMeshProUGUI _expTmp;
    [SerializeField] private Color _textBeginColor;
    [SerializeField] private Color _textEndColor;
    private Sequence _seq;
    private int _prevLevel = -1;
    public void ResetGauge()
    {
        _gaugeFill.fillAmount = 0;
    } // 디버그 안해봄
    
    public void Refresh(int currentLevel, int currentExp, int max)
    {
        if (_prevLevel < currentLevel)
        {
            _prevLevel = currentLevel;
            _levelTmp.color = Color.Lerp(_textBeginColor, _textEndColor, (float)currentLevel/_maxLevel);
            _levelTmp.text = currentLevel.ToString();
            ResetGauge();
        }
        currentExp = Mathf.Clamp(currentExp, 0, max);
        float ratio = (float)currentExp / max;

        _expTmp.text = $"{((int)ratio * 100).ToString()}%";
        
        _seq = DOTween.Sequence();
        _seq.Append(_gaugeFill.DOFillAmount(ratio, _duration).SetUpdate(true));
    }
}