using UnityEngine;

namespace WeaponManage
{
    
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponInfoSO weaponInfo;

        public abstract void Initialize();
    }
}