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
   
    private Agent _owner;
    private Rigidbody _rigid;
    public void Initialize(Agent agent)
    {
        _owner = agent;
        maxHealth = _owner.Stat.maxHealth.GetValue();
        _currentHealth = maxHealth; //  최대체력으로 세팅

    }
    
    public void TakeDamage(int amount)
    {
        if (_owner.Stat.isResist) return;
        amount = Mathf.Clamp(amount - _owner.Stat.defence.GetValue(), 0, 500);
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