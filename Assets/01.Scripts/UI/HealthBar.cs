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

        float prevFill = (float)prevValue / max;
        float newFill = (float)newValue / max;
        if (prevValue > newValue)
        { // 감소한 경우
            ShakeBar();
            StartCoroutine(DecreaseCoroutine(prevFill, newFill));
        }
        else
        { // 증가한 경우
            StartCoroutine(IncreaseCoroutine(prevFill, newFill));
        }
        

    }

    private IEnumerator DecreaseCoroutine(float before, float target)
    {
        yield return StartCoroutine(MainGaugeFillCoroutine(before, target));
        yield return new WaitForSeconds(_changeTerm);
        StartCoroutine(SubGaugeFillCoroutine(before, target));
    }
    
    private IEnumerator IncreaseCoroutine(float before, float target)
    {
        yield return StartCoroutine(SubGaugeFillCoroutine(before, target));
        yield return new WaitForSeconds(_changeTerm);
        StartCoroutine(MainGaugeFillCoroutine(before, target));
    }

    private IEnumerator SubGaugeFillCoroutine(float before, float target)
    {
        float currentTime = 0;
        while (currentTime < _changeTime)
        {
            currentTime += Time.deltaTime;
            _subFillBar.fillAmount = Mathf.Lerp(before, target, currentTime / _changeTime); 
            yield return null;
        }

        _subFillBar.fillAmount = target;
    }
    private IEnumerator MainGaugeFillCoroutine(float before, float target)
    {
        float currentTime = 0;
        while (currentTime < _changeTime)
        {
            currentTime += Time.deltaTime;
            _mainFillBar.fillAmount = Mathf.Lerp(before, target, currentTime / _changeTime); 
            yield return null;
        }

        _mainFillBar.fillAmount = target;
    }

    public void ShakeBar()
    {
        if (_isShaking) return;
        _isShaking = true;
        _currentShakeTween = _rectTrm.DOShakeAnchorPos(0.1f).OnComplete(() => { _isShaking = false;});
    }

}
