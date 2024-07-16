using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _mainFillBar;
    [SerializeField] private Image _subFillBar;

    [Header("Gauge Setting")] 
    [SerializeField] private float _changeTerm = 0.2f;
    [SerializeField] private float _changeTime = 0.5f;
    [SerializeField] private Agent _owner;
    
    private RectTransform _rectTrm;
    private Tween _currentShakeTween;
    private bool _isShaking;


    private Sequence _seq;
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
    }

    private void Start()
    {
        _owner.HealthCompo.OnHealthChangedValueEvent += HandleHealthChange;

    }

    public void HandleHealthChange(int prevValue, int newValue, int max)
    {
        if(prevValue == newValue) return;

        //float prevFill = (float)prevValue / max;
        float newFill = (float)newValue / max;
        _seq = DOTween.Sequence();
        if (prevValue > newValue)
        { // 감소한 경우
            ShakeBar();
            print("newfill: "+ newFill);
            _seq.Append(_mainFillBar.DOFillAmount(newFill, _changeTime));
            _seq.AppendInterval(_changeTerm);
            _seq.Append(_subFillBar.DOFillAmount(newFill, _changeTime));
        }
        else
        { // 증가한 경우
            _seq.Append(_subFillBar.DOFillAmount(newFill, _changeTime));
            _seq.AppendInterval(_changeTerm);
            _seq.Append(_mainFillBar.DOFillAmount(newFill, _changeTime));

        }
        

    }

   
    public void ShakeBar()
    {
        if (_isShaking) return;
        _isShaking = true;
        _currentShakeTween = _rectTrm.DOShakeAnchorPos(0.1f).OnComplete(() => { _isShaking = false;});
    }

}
