using System;
using UnityEngine;

public class TracingProjectile : Projectile
{
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private bool _isPlayerTarget;

    [Header("Tracing Setting")] 
    [SerializeField] private float _accuracy = 2f;


    private void Start()
    {
        if (_isPlayerTarget)
        {
            // 플레이어 트랜스폼을 싱글톤으로 넣으면 된다
        }
    }

    public void SetTarget(Transform target)
    {
        _targetTrm = target;
    }

    protected override bool Update()
    {
        if (!base.Update()) return false;
        
        TraceTarget();
        return true;
    }

    private void TraceTarget()
    {
        if (_targetTrm == null)
            return;
        Vector2 targetDir = _targetTrm.position - transform.position;
        Vector2 newDir = _direction + ((targetDir.normalized) * _accuracy);
        _rigidCompo.velocity = newDir * _speed;
        _direction = newDir.normalized;
        _visualTrm.right = newDir;
    }
}