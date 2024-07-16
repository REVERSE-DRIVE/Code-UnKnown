﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDirectionArrow : MonoBehaviour
{
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    
    public Transform Target
    {
        get => _target;
        set
        {
            if (value.gameObject.activeSelf == false)
            {
                _target = null;
                _arrowImage.enabled = false;
            }
            else
            {
                _target = value;
            }
        }
    }

    private void Update()
    {
        EnemyIsDead();
        LookToTarget();
        OnScreenArrow();
        ArrowActive();
    }

    private void EnemyIsDead()
    {
        if (_target == null) return;
        if (_target.TryGetComponent(out EnemyBase enemyBase))
        {
            if (enemyBase.isDead)
            {
                _target = null;
                _arrowImage.enabled = false;
            }
        }
    }

    private void ArrowActive()
    {
        if (_target == null) return;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(_target.position);
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            _arrowImage.enabled = true;
        }
        else
        {
            _arrowImage.enabled = false;
        }
    }

    private void OnScreenArrow()
    {
        if (_target == null) return;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(_target.position);
        screenPos.x = Mathf.Clamp(screenPos.x, 0.1f, 0.9f);
        screenPos.y = Mathf.Clamp(screenPos.y, 0.1f, 0.9f);
        
        float x = (screenPos.x - 0.5f) * Screen.width;
        float y = (screenPos.y - 0.5f) * Screen.height;
        
        Vector3 pos = new Vector3(x, y, 0);
        
        transform.localPosition = pos;
    }

    private void LookToTarget()
    {
        if (_target == null) return;    
        Vector3 dir = _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}