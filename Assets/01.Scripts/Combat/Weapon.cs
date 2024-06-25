using System;
using UnityEngine;

namespace WeaponManage
{
    
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponInfoSO weaponInfo;
        [SerializeField] protected ParticleSystem _attackParticle;

        public Action OnAttackEvent;
        public abstract void Initialize();

        protected virtual void Awake()
        {
            OnAttackEvent += HandleAttackEvent;
        }

        public void Attack()
        {
            OnAttackEvent?.Invoke();
        }

        protected abstract void HandleAttackEvent();

        public abstract void HandleRotateWeapon(Vector2 direction);
    }
}