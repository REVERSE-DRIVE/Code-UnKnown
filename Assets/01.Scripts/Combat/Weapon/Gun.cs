using System;
using ObjectPooling;
using UnityEngine;

namespace WeaponManage
{
    public abstract class Gun : Weapon
    {
        public GunsWeaponInfoSO gunInfo { get; protected set; }
        [SerializeField] protected PoolingType _projectilePoolType;
        [SerializeField] protected LayerMask _whatIsEnemy;
        [SerializeField] protected float _radius;
        protected Collider2D[] _targetColliders;
        protected int _currentBullets;
        

        protected override void Awake()
        {
            base.Awake();
            _targetColliders = new Collider2D[1];
            gunInfo =  weaponInfo as GunsWeaponInfoSO;
            _currentBullets = gunInfo.maxBullets;
        }
        
        protected override void HandleAttackEvent()
        {
            if (_currentBullets <= 0) return;
            _currentBullets--;
            AttackLogic();
        }

        private void Update()
        {
            LookToTarget();
        }

        protected void LookToTarget()
        {
            int hits = Physics2D.OverlapCircleNonAlloc
                (transform.position, weaponInfo.range, _targetColliders, _whatIsEnemy);
            if (hits > 0)
            {
                Vector2 direction = (Vector2)_targetColliders[0].transform.position - (Vector2)transform.position;
                _controlDirection = direction.normalized;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, weaponInfo.range);
        
        }

        protected abstract void AttackLogic();
        
        protected void Reload()
        {
            _currentBullets = gunInfo.maxBullets;
        }
        
        
    }
}