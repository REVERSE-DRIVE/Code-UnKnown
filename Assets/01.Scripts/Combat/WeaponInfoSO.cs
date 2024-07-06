using UnityEngine;

namespace WeaponManage
{
    public class WeaponInfoSO : ScriptableObject
    {
        public int id;
        public string weaponName;
        public int damage;
        public float coolTime;
        public float range;
        [SerializeField] private Weapon weaponPrefab;

        public Weapon WeaponPrefab
        {
            get
            {
                weaponPrefab.weaponInfo = this;
                return weaponPrefab;
            }
        }
    }
    
}