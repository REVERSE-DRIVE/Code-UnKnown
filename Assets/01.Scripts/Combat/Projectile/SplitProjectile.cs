using Calculator;
using ObjectPooling;
using UnityEngine;

public class SplitProjectile : Projectile
{ 
    [Header("Split Setting")]
    [SerializeField]
    [Range(2, 16)]
    private short _splitAmount = 2;

    [SerializeField] private PoolingType _splitProjectile;

    protected override void Awake()
    {
        base.Awake();
        OnDestroyEvent += DestroyEvent;
    }

    protected void DestroyEvent()
    {
        Vector2[] directions = VectorCalculator.DirectionsFromCenter(_splitAmount);
        for (short i = 0; i < _splitAmount; i++)
        {
            Projectile projectile = PoolingManager.Instance.Pop(_splitProjectile) as Projectile;
            projectile.Shoot(directions[i]);
        }
    }
}