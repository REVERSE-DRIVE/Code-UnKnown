using System;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnValueChangedEvent(int prevValue, int newValue, int max);

public class Health : MonoBehaviour, IDamageable, IHealable
{
    public UnityEvent OnHealthChangedEvent;
    public UnityEvent OnDieEvent;
    public OnValueChangedEvent OnHealthChangedValueEvent;
    
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
        HandleHealthChange(-amount);
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        CheckDie();
        HandleHealthChange(amount);
    }

    public void SetHealth(int amount)
    {
        int before = _currentHealth;
        _currentHealth = amount;
        HandleHealthChange(before > _currentHealth ? before - _currentHealth : _currentHealth - before);
    }

    private void HandleHealthChange(int change)
    {
        OnHealthChangedEvent?.Invoke();
        print($"체력 갱신 - {gameObject.name}");
        OnHealthChangedValueEvent?.Invoke(_currentHealth-change, _currentHealth, maxHealth);
    }

    public void CheckDie()
    {
        if (_currentHealth <= 0)
        {
            OnDieEvent?.Invoke();
        }
    }
}