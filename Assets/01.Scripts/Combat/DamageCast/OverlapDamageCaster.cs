using System;
using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    private Collider2D[] hits;
    public event Action OnCastEvent;

    private void Awake()
    {
        hits = new Collider2D[10];
    }

    public override void CastDamage(float radius, int damage)
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, radius, hits, _whatIsAgent);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].TryGetComponent(out Agent agent))
            {
                agent.HealthCompo.TakeDamage(damage);
                OnCastEvent?.Invoke();
            }
        }
    }
}