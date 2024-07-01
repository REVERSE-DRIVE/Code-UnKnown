using ObjectPooling;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    private float _speed = 10f;
    
    public void Shot(Vector3 direction)
    {
        transform.Translate(direction * (_speed * Time.deltaTime));
    }
    public override void ResetItem()
    {
        
    }
}