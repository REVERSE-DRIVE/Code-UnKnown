using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetectObject : ConditionObject
{
    [SerializeField] private float _detectRange;
    [SerializeField] private LayerMask _playerLayer;
    private Collider2D[] hits;

    private void Awake()
    {
        hits = new Collider2D[1];
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (_isActive) return;
        int amount =Physics2D.OverlapCircleNonAlloc(transform.position, _detectRange, hits, _playerLayer);
        if (amount >= 1)
        {
            _isActive = true;
            OnActiveEvent?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}