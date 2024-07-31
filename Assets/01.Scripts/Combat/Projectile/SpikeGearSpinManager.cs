using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SpikeGearSpinManager : MonoBehaviour
{
    [SerializeField] private List<SpikeGear> _gears;
    [SerializeField] private float _spinSpeed = 100f;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private float _spinRadius = 1f;
    
    private List<Vector3> _deltaPositionList;
    private bool _isSpinning = false;

    private void Awake()
    {
        _gears = GetComponentsInChildren<SpikeGear>().ToList();
    }

    private void Start()
    {
        _deltaPositionList = new List<Vector3>();
        for (int i = 0; i < _gears.Count; i++)
        {
            _deltaPositionList.Add(Vector3.zero);
        }
    }
    
    private void CalculateOffset()
    {
        float angleStep = 360f / _gears.Count;

        for (int i = 0; i < _gears.Count; i++)
        {
            float currentAngle = angleStep * i * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * _spinRadius;
            _deltaPositionList[i] = pos;
        }
    }
    
    public void StartSpin()
    {
        if (_isSpinning) return;
        _isSpinning = true;
        CalculateOffset();
        
        Sequence seq = DOTween.Sequence();

        for(int i = 0; i < _gears.Count; i++)
        {
            SpikeGear spikeGear = _gears[i];
            Vector3 pos = _deltaPositionList[i];

            spikeGear.transform.localPosition = Vector3.zero;
            spikeGear.transform.localScale = Vector3.one * 0.5f;
            spikeGear.gameObject.SetActive(true);

            seq.Join(spikeGear.transform.DOLocalMove(pos, _moveDuration));
        }

        seq.OnComplete(StartSkill);
    }

    private void StartSkill()
    {
        for (int i = 0; i < _gears.Count; i++)
        {
            float initialAngle = (360f / _gears.Count) * i;
            _gears[i].SetRotate(true, _spinSpeed, _spinRadius, initialAngle);
        }
    }

    public void StopRotation()
    {
        for (int i = 0; i < _gears.Count; i++)
        {
            _gears[i].SetRotate(false, 0, 0, 0);
        }
        _isSpinning = false;
        
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < _gears.Count; i++)
        {
            SpikeGear spikeGear = _gears[i];
            seq.Join(spikeGear.transform.DOLocalMove(Vector3.zero, _moveDuration));
        }
        seq.OnComplete(() =>
        {
            for (int i = 0; i < _gears.Count; i++)
            {
                _gears[i].gameObject.SetActive(false);
            }
        });
    }
}
