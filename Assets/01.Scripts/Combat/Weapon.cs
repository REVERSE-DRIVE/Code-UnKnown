using System;
using UnityEngine;

namespace WeaponManage
{
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponInfoSO weaponInfo;
        [SerializeField] protected ParticleSystem _attackParticle;
        [SerializeField] protected Animator _animatorCompo;
        public Action OnAttackEvent;
        public abstract void Initialize();
        protected Vector2 _controlDirection;
        protected Vector2 _previousDirection;
        protected bool _isWeaponRotateLock = false;


        protected virtual void Awake()
        {
            OnAttackEvent += HandleAttackEvent;
        }

        public void Attack()
        {
            OnAttackEvent?.Invoke();
        }

        protected abstract void HandleAttackEvent();

        public void HandleRotateWeapon(Vector2 direction)
        {
            _controlDirection = direction;
            if (_isWeaponRotateLock) return;
            if (direction.sqrMagnitude == 0)
                return;
            // 오프셋 부분 수정해야될 수도 있움
            Quaternion rotate = Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            transform.rotation = rotate;
            if (Mathf.Abs(rotate.z) > 0.7f)  // z rotation값 재계산 해야함
            {
                transform.parent.localScale = new Vector2(-1, 1);
                transform.localScale = -Vector2.one;
            }
            else
            {
                transform.parent.localScale = Vector2.one;
                transform.localScale = Vector2.one;
            }
        }
    }
}