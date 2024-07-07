using System;
using ObjectPooling;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    public Action OnHealthChangedEvent;
    public Action OnDieEvent;
    
    [SerializeField] private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    public int maxHealth;
   
    public void Initialize(int max)
    {
        maxHealth = max;
        _currentHealth = max;
    }
    
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        CheckDie();
        OnHealthChangedEvent?.Invoke();
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        CheckDie();
        OnHealthChangedEvent?.Invoke();
    }

    public void SetHealth(int amount)
    {
        _currentHealth = amount;
        OnHealthChangedEvent?.Invoke();
    }

    public void CheckDie()
    {
        if (_currentHealth <= 0)
        {
            OnDieEvent?.Invoke();
        }
    }
}