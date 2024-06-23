using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class Projectile : MonoBehaviour, ILifeTimeLimited, IPoolable
{
    [field: SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;

    [SerializeField] protected PoolingType _destroyParticlePoolingType;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;

    private Rigidbody2D _rigidCompo;
    private bool _isActive;
    
    protected float _currentLifeTime = 0;
    float ILifeTimeLimited.CurrentLifeTime
    {
        get => _currentLifeTime;
        set => _currentLifeTime = value;
    }

    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();
        
    }

    protected virtual void Update()
    {
        if (!_isActive) return;
        
        _currentLifeTime += Time.deltaTime;
    }
    
    
    public void Initialize(int damage, float speed, float lifeTime)
    {
        this._damage = damage;
        this._speed = speed;
        this._lifeTime = lifeTime; 
        
    }

    public void Shoot(Vector2 direction)
    {
        _rigidCompo.velocity = direction * _speed;
        _isActive = true;
    }
    

    public void ResetItem()
    {
        _isActive = false;
        _currentLifeTime = 0;

    }

    public void CheckDie()
    {
        if (_currentLifeTime >= _lifeTime)
        {
            HandleDie();
        }
    }

    public void HandleDie()
    {
        PoolingManager.Instance.Pop(_destroyParticlePoolingType);
        PoolingManager.Instance.Push(this);
    }
}
