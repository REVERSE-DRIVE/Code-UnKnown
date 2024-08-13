using UnityEngine;

public class ReflectProjectile : Projectile
{
    [SerializeField] private int _reflectCount;
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_reflectCount > 0)
            {
                _reflectCount--;
                Vector2 reflectDir = Vector2.Reflect(_rigidCompo.velocity.normalized, other.transform.position - transform.position);
                _rigidCompo.velocity = reflectDir * _speed;
            }
            else
            {
                base.OnTriggerEnter2D(other);
            }
        }
        else
        {
            base.OnTriggerEnter2D(other);
        }
    }
}