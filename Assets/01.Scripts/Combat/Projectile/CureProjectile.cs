using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureProjectile : Projectile
{
    protected override bool Update()
    {
        base.Update();
        
        if (_currentLifeTime >= _lifeTime)
            Destroy(gameObject);

        return true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEnemy && other.CompareTag("Enemy")) return;
        if (!_isEnemy && other.CompareTag("Player")) return;
        if (other.transform.TryGetComponent(out IDamageable health))
        {
            health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
