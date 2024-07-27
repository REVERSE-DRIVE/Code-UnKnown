using UnityEngine;

public class BombProjectile : Projectile
{
    [Header("Explosion Settings")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _targetLayer;
    
    private Collider2D[] _attackTarget;

    protected override void Awake()
    {
        base.Awake();
        _attackTarget = new Collider2D[10];
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        BombExplosion();
    }

    private void BombExplosion()
    {
        int target = Physics2D.OverlapCircleNonAlloc(transform.position, _explosionRadius, _attackTarget, _targetLayer);
        for (int i = 0; i < target; i++)
        {
            IDamageable damageable = _attackTarget[i].GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }
        HandleDie();
    }
}