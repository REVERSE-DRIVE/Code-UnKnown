using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GearSpinManager : MonoBehaviour
{
    [SerializeField] protected List<Gear> _gears;
    [SerializeField] protected float _spinSpeed = 100f;
    [SerializeField] protected float _moveDuration = 1f;
    [SerializeField] protected float _spinDuration = 1f;
    [SerializeField] protected float _spinRadius = 1f;
    
    private List<Vector3> _deltaPositionList;
    private bool _isSpinning = false;
    
    protected virtual void Start()
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
            Gear gear = _gears[i];
            Vector3 pos = _deltaPositionList[i];

            gear.transform.localPosition = Vector3.zero;
            gear.transform.localScale = Vector3.one * 0.5f;
            gear.gameObject.SetActive(true);

            seq.Join(gear.transform.DOLocalMove(pos, _moveDuration));
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
        StartCoroutine(StopRotation());
    }

    private IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(_spinDuration);
        for (int i = 0; i < _gears.Count; i++)
        {
            _gears[i].SetRotate(false, 0, 0, 0);
        }
        _isSpinning = false;
        
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < _gears.Count; i++)
        {
            Gear gear = _gears[i];
            seq.Join(gear.transform.DOLocalMove(Vector3.zero, _moveDuration));
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
