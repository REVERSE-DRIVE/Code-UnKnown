using ObjectPooling;
using UnityEngine;

namespace WeaponManage
{
    [CreateAssetMenu(menuName = "SO/WeaponInfo/Gun")]
    public class GunsWeaponInfoSO : WeaponInfoSO
    {
        public PoolingType projectilePoolType;
        public int maxBullets;
        

    }
}