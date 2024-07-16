using ObjectManage;

namespace WeaponManage
{
    public class DataAXE : Sword
    {
        protected override void AttackLogic()
        {
            DirectionVFXObject dirVFX = PoolingManager.Instance.Pop(_attackVFX) as DirectionVFXObject;
            dirVFX.Initialize(new ActionData { direction = _controlDirection, origin = transform.position });
            
            for (int i = 0; i < _targetAmount; i++)
            {
                if (_targetColliders[i].TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(swordInfo.damage);
                    //GenerateHitVFX(_targetColliders[i].transform.position);
                }
            }
        }
    }
}