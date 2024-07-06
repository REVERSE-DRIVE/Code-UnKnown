﻿using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] private Transform _centerTrm;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isRotate;
    [SerializeField] private float _radius = 1f; // 원운동의 반지름
    
    private float _angle; // 현재 각도
    
    public void SetRotate(bool value, float speed, float radius, float initialAngle)
    {
        _isRotate = value;
        _speed = speed;
        _radius = radius;
        _angle = initialAngle;
    }

    private void Update()
    {
        if (_isRotate)
        {
            _angle += _speed * Time.deltaTime;
            float radians = _angle * Mathf.Deg2Rad;
            float x = _centerTrm.position.x + Mathf.Cos(radians) * _radius;
            float y = _centerTrm.position.y + Mathf.Sin(radians) * _radius;
            transform.position = new Vector2(x, y);
        }
    }
}