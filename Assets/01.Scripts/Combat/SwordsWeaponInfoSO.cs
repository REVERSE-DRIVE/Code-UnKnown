using UnityEngine;

namespace WeaponManage
{
    [CreateAssetMenu(menuName = "SO/WeaponInfo/Sword")]
    public class SwordsWeaponInfoSO : WeaponInfoSO
    {
        public int limitTargetAmount = 5;
        public float attackRadius; // overlap 반지름
        public float attackOffset; // overlap 중심점 
    }
}