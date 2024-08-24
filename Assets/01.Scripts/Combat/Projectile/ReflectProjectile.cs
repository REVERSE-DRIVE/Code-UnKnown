using UnityEngine;

public class ReflectProjectile : Projectile
{
    [SerializeField] private int _reflectCount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEnemy && other.CompareTag("Enemy")) return;
        if (_reflectCount <= 0 || other.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            return;
        }
        Vector2 normal = other.transform.position - transform.position;
        Vector2 reflectDir = Reflect(_rigidCompo.velocity.normalized, normal.normalized);
        _rigidCompo.velocity = reflectDir * _speed;
        _reflectCount--;
    }

    private Vector2 Reflect(Vector2 inDir, Vector2 normal)
    {
        return Vector2.Reflect(inDir, normal);
    }
    
    public override void ResetItem()
    {
        base.ResetItem();
        _reflectCount = 3;
    }
}