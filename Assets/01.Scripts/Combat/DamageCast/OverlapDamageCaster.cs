using System;
using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    [SerializeField] private float _radius = 3f;
    private Collider2D[] hits;
    public event Action OnCastEvent;

    private void Awake()
    {
        hits = new Collider2D[10];
    }

    public override void CastDamage()
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, hits, _whatIsAgent);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].TryGetComponent(out Agent agent))
            {
                agent.HealthCompo.TakeDamage(_damage);
                OnCastEvent?.Invoke();
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.white;
    }
}