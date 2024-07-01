using System;
using UnityEngine;

namespace ObjectManage
{
    
    public class FieldObject : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int _currentHealth;

        public event Action OnDestroyEvent;
        // 파티클 뿌리는거 해야됨

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            CheckDie();
        }

        public void RestoreHealth(int amount)
        {
            _currentHealth += amount;
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
