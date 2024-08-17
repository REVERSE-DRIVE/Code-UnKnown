using UnityEngine;

namespace ObjectManage
{
    public abstract class DestroyableObject : FieldObject, IDamageable
    {
        [SerializeField] protected int _currentHealth;
        [SerializeField] protected int _maxHealth;
        public int MaxHealth => _maxHealth;
        
        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            CheckDie();
        }

        public void CheckDie()
        {
            if (_currentHealth <= 0)
            {
                OnDestroyEvent?.Invoke();
            }
        }
    }
}