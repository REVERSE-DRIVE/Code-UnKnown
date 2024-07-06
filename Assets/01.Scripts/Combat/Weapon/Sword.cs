using UnityEngine;

namespace WeaponManage
{
    public abstract class Sword : Weapon
    {
        public SwordsWeaponInfoSO swordInfo { get; protected set; }

        protected Collider2D[] _targetColliders;
        protected int _targetAmount = 0;
        protected LayerMask _targetLayer; // enemy와 projectile타입이 해당된다

        
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

        protected void DetectTargets()
        {
            Vector2 origin = (Vector2)transform.position + (swordInfo.attackOffset * _controlDirection.normalized);
            int amount = Physics2D.OverlapCircleNonAlloc(
                origin, swordInfo.attackRadius, _targetColliders, _targetLayer);

            _targetAmount = amount;

        }

       
    }
}