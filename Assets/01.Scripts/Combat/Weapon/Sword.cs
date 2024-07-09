using System;
using ObjectManage;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeaponManage
{
    public abstract class Sword : Weapon
    {
        public SwordsWeaponInfoSO swordInfo { get; protected set; }
        [SerializeField] protected PoolingType _hitVFXPoolType;
        [SerializeField] protected PoolingType _attackVFX;
        protected Collider2D[] _targetColliders;
        protected int _targetAmount = 0;
        protected LayerMask _targetLayer; // enemy와 projectile타입이 해당된다
        protected Vector2 _origin;
        
        protected override void Awake()
        {
            base.Awake();
            swordInfo =  weaponInfo as SwordsWeaponInfoSO;
            _targetColliders = new Collider2D[swordInfo.limitTargetAmount];
            _targetLayer = LayerMask.GetMask("Enemy", "Projectile");
        }
        
        
        protected override void HandleAttackEvent()
        {
            DetectTargets();
            AttackLogic();
        }

        protected abstract void AttackLogic();

        protected virtual void DetectTargets()
        {
            _origin = (Vector2)transform.position + (swordInfo.attackOffset * _controlDirection.normalized);
            int amount = Physics2D.OverlapCircleNonAlloc(
                _origin, swordInfo.attackRadius, _targetColliders, _targetLayer);

            _targetAmount = amount;
        }

        protected void GenerateHitVFX(Vector2 position)
        {
            EffectObject effect = PoolingManager.Instance.Pop(_hitVFXPoolType) as EffectObject;
            Vector2 newPosition = position + Random.insideUnitCircle;
            effect.transform.position = newPosition;
            if (effect == null)
            {
                Debug.LogWarning("EffectObject PoolType이 잘못되었습니다");
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                (Vector2)transform.position + (swordInfo.attackOffset * _controlDirection.normalized),
                swordInfo.attackRadius);
        }
    }
}