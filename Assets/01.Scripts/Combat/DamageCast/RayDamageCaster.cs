﻿using System;
using UnityEngine;

public class RayDamageCaster : DamageCaster
{
    [SerializeField] private float _distance = 3f;
    public event Action OnCastEvent;

    public Vector2 Direction
    {
        get => transform.right;
        set => transform.right = value;
    }
    public override void CastDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, _distance, _whatIsAgent);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Agent agent))
            {
                agent.HealthCompo.TakeDamage(_damage);
                OnCastEvent?.Invoke();
            }
        }
    }
}