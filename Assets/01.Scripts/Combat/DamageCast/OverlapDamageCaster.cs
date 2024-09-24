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

    public override bool CastDamage(int damage, float radius = 1)
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, radius, hits, _whatIsAgent);
        if (hitCount == 0) return false;
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].TryGetComponent(out IDamageable agent))
            {
                agent.TakeDamage(damage);
                OnCastEvent?.Invoke();
            }
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}