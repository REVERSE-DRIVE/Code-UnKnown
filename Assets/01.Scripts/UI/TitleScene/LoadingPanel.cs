using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoadingPanel : MonoBehaviour, IWindowPanel
{
    private RectTransform _rectTrm;
    [SerializeField] private float _endSize;
    [SerializeField] private float _duration;
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
    }

    public void Open()
    {
        _rectTrm.sizeDelta = Vector2.zero;
        _rectTrm.DOSizeDelta(new Vector2(_endSize, _endSize), _duration);
    }

    public void Close()
    {
        _rectTrm.sizeDelta = Vector2.zero;
    }
}
