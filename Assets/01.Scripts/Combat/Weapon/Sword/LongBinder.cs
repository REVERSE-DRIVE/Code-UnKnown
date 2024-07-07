using UnityEngine;

namespace WeaponManage
{
    public class LongBinder : Sword
    {
        protected override void AttackLogic()
        {
            for (int i = 0; i < _targetAmount; i++)
            {
                print(_targetColliders[i].transform.name +"피격");
                if (_targetColliders[i].TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(swordInfo.damage);
                    GenerateHitVFX(_targetColliders[i].transform.position);
                }
            }
        }
    }
}