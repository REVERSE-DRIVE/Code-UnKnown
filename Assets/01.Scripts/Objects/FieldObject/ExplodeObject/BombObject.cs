using UnityEngine;

public class BombObject : ExplodeObject, IDamageable
{
    [SerializeField] private int _leftExplodeCount;

    protected override void Awake()
    {
        base.Awake();
        OnExplodeEvent += CheckDie;
    }

    public void TakeDamage(int amount)
    {
        Explode();
        _leftExplodeCount--;
    }

    public void CheckDie()
    {
        if (_leftExplodeCount <= 0)
        {
            HandleDie();
        }
    }

    public virtual void HandleDie()
    {
        Destroy(gameObject);
    }
}