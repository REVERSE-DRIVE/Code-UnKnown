using UnityEngine;

namespace WeaponManage
{
    [CreateAssetMenu(menuName = "SO/WeaponInfo")]
    public class WeaponInfoSO : ScriptableObject
    {
        public int id;
        public int damage;
        public float coolTime;
        public float range;
        
    }
    
}