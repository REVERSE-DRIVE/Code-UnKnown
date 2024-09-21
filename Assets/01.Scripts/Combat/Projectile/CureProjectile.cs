using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureProjectile : Projectile
{
    Transform target;

    public void Shoot(Transform _target)
    {
        target = _target;
        _isActive = true;
    }

    protected override bool Update()
    {
        base.Update();

        if (target) {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.z = 0;
            
            _rigidCompo.velocity = direction * _speed;
            transform.right = direction;
        }
        
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
