using DG.Tweening;
using UnityEngine;

public class MinimapPanel : UIPanel
{
    [SerializeField] private float _defaultYDelta;
    [SerializeField] private float _activetYDelta;
    private RectTransform _rectTrm;

    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }

    public override void Open()
    {
        base.Open();
        _rectTrm.DOAnchorPosX(_activetYDelta, _activeDuration);
    }

    public override void Close()
    {
        base.Close();
        _rectTrm.DOAnchorPosX(_defaultYDelta, _activeDuration);

        
    }
}