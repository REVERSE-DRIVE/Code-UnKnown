using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour, IDamageable
{
    public Action OnShieldBreakEvent;
    [SerializeField] private int _durability;
    private bool _isActive;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private ParticleSystem _particle;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    public void SetShield(int durability, bool isApply = true)
    {
        if (isApply)
        {
            _durability += durability;
        }
        else
        {
            _durability = durability;
        }
        ShieldActive(true);
    }

    private void ShieldActive(bool value)
    {
        _isActive = value;
        _collider.enabled = value;
        _spriteRenderer.enabled = value;
        if(value) 
            _particle.Play();
        else 
            _particle.Stop();
    }
    
    
    public void TakeDamage(int amount)
    {
        if (!_isActive) return;
        _durability -= amount;
        CheckDie();
    }

    public void CheckDie()
    {
        if (_durability <= 0)
        {
            ShieldActive(false);
            OnShieldBreakEvent?.Invoke();
        }
    }
}