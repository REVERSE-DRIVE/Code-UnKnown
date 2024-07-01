using UnityEngine;

namespace WeaponManage
{
    public abstract class Sword : Weapon
    {
        public SwordsWeaponInfoSO swordInfo { get; protected set; }

        protected Collider2D _targetColliders;
        protected LayerMask _targetLayer; // enemy와 projectile타입이 해당된다
        
        protected override void Awake()
        {
            base.Awake();
            swordInfo =  weaponInfo as SwordsWeaponInfoSO;
            
            _targetLayer = 
        }
        public override void Initialize()
        {
            
        }

        protected abstract void DetectTargets();


    }
}