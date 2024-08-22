using System;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class Projectile : MonoBehaviour, ILifeTimeLimited, IPoolable, IDamageable
{
    [field: SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    public event Action OnDestroyEvent;
    
    [SerializeField] protected PoolingType _destroyParticlePoolingType;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected bool _isEnemy = true;
    protected Rigidbody2D _rigidCompo;
    protected bool _isActive;
    protected float _currentLifeTime = 0;
    protected Transform _visualTrm;
    protected Vector2 _direction;
    float ILifeTimeLimited.CurrentLifeTime
    {
        get => _currentLifeTime;
        set => _currentLifeTime = value;
    }

    protected virtual void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();
        _visualTrm = transform.Find("Visual");
    }

    protected virtual bool Update()
    {
        if (!_isActive) return false;
        
        _currentLifeTime += Time.deltaTime;
        return true;
    }

    [ContextMenu("DebugShootLeft")]
    private void DebugShoot()
    {
        Shoot(new Vector2(10, 10));
    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
    }
    
    public void Initialize(Vector2 position, int damage, float speed, float lifeTime, bool isEnemy = true)
    {
        transform.position = position;
        this._damage = damage;
        this._speed = speed;
        this._lifeTime = lifeTime;
        this._isEnemy = isEnemy;
    }

    public void Shoot(Vector2 direction)
    {
        _direction = direction;
        _rigidCompo.velocity = direction * _speed;
        transform.right = direction;
        _isActive = true;
    }
    

    public virtual void ResetItem()
    {
        _isActive = false;
        _currentLifeTime = 0;

    }
    
    

    public void TakeDamage(int amount)
    {
        HandleDie();
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
        EffectObject effect = PoolingManager.Instance.Pop(_destroyParticlePoolingType) as EffectObject;
        effect.transform.position = transform.position;
        OnDestroyEvent?.Invoke();
        _isActive = false;
        PoolingManager.Instance.Push(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEnemy && other.CompareTag("Enemy")) return;
        if (!_isEnemy && other.CompareTag("Player")) return;
        if (other.transform.TryGetComponent(out IDamageable health))
        {
            health.TakeDamage(_damage);
        }
        HandleDie();
    }
}
