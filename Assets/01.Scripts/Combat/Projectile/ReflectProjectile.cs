using UnityEngine;

public class ReflectProjectile : Projectile
{
    [SerializeField] private int _reflectCount;
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Take Damage
        if (_isEnemy && other.CompareTag("Enemy")) return;
        if (!_isEnemy && other.CompareTag("Player")) return;
        if (other.transform.TryGetComponent(out IDamageable health))
        {
            health.TakeDamage(_damage);
        }
        // Reflect
        if (_reflectCount > 0)
        {
            _reflectCount--;
            Vector2 reflectDir = Reflect(_rigidCompo.velocity.normalized, other.transform.right);
            _rigidCompo.velocity = reflectDir * _speed;
        }
        else
        {
            base.OnTriggerEnter2D(other);
        }
    }

    private Vector2 Reflect(Vector2 inDir, Vector2 normal)
    {
        return Vector2.Reflect(inDir, normal);
    }
}