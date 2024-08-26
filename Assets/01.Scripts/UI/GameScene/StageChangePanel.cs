using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StageChangePanel : UIPanel
{
    [SerializeField] private Transform _sizingPanel;
    [SerializeField] private Image _gauge;
    [SerializeField] private float _sizingTargetValue = 25f; 
    [SerializeField] private float _sizingDuration;
    public override void Open()
    {
        SetCanvasGroup(true);
        _sizingPanel.DOScale(_sizingTargetValue, _sizingDuration);
        

    }

    public override void Close()
    {
        
        _sizingPanel.DOScale(0f, _sizingDuration).OnComplete(()=> SetCanvasGroup(false));
            
        
    }

    public void SetZeroGauge()
    {
        _gauge.fillAmount = 0f;
    }

    public void FillGauge()
    {
        _gauge.DOFillAmount(1f, 2f);
    }
}