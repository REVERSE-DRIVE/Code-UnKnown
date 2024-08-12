using System;
using System.Collections;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class ExplodeObject : FieldObject
{
    public event Action OnExplodeEvent;

    [Header("Explode Setting")] 
    [SerializeField] protected float _explodeRadius;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delay;
    [SerializeField] private PoolingType _explodeVFXPoolingType;
    [SerializeField] protected LayerMask _targetLayer;
    private Collider2D[] _targets = new Collider2D[10];
    protected virtual void Awake()
    {
        OnExplodeEvent += HandleRangeDamage;
    }

    public void Explode()
    {
        StartCoroutine(ExplodeCoroutine());
        
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        EffectObject effect = PoolingManager.Instance.Pop(_explodeVFXPoolingType) as EffectObject;
        effect.Initialize(transform.position);
        effect.Play();
        (PoolingManager.Instance.Pop(PoolingType.ExplodeMark) as EffectObject).Initialize(transform.position);
        OnExplodeEvent?.Invoke();
       
    }

    protected virtual void HandleRangeDamage()
    {
        int amount = Physics2D.OverlapCircleNonAlloc(transform.position, _explodeRadius, _targets, _targetLayer);
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            if (_targets[i].TryGetComponent(out IDamageable hit))
            {
                hit.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }
}
