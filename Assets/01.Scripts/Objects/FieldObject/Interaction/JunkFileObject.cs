using System;
using ObjectManage;
using UnityEngine;

public class JunkFileObject : InteractObject
{
    private Rigidbody2D _rigidCompo;
    [Serializable] private bool _collisionDestroy;
    [SerializeField] private float _pushPower;
    private Vector2 _origin;
    [SerializeField] private int _damage = 3;
    private bool _isActive;
    
    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();

        OnInteractEvent += HandlePush;
    }

    private void Update()
    {
        if (!_isActive)
            return;
            
        float magnitude = _rigidCompo.velocity.magnitude;
        if (magnitude <= 0.1f)
        {
            _isActive = false;
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_collisionDestroy) return;
        if(other.transform.TryGetComponent(out Health health))
            HandleCollisionEvent(health);
    }

    public override void Interact(InteractData data)
    {
        base.Interact(data);
        _origin = data.interactOriginPosition;
    }

    private void HandlePush()
    {
        Vector2 direction = (Vector2)transform.position - _origin;
        _rigidCompo.AddForce(direction.normalized * _pushPower, ForceMode2D.Impulse);
    }

    private void HandleCollisionEvent(Health hit)
    {
        hit.TakeDamage(_damage);
    }
    
    
}