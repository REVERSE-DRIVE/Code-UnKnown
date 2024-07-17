using System;
using UnityEngine;
using WeaponManage;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/ItemSO", fileName = "ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public ItemType itemType;
        public int id;
        public string itemName;
        public Sprite itemIcon;
        public ResourceRank resourceRank;
        public WeaponInfoSO weaponInfoSO;

        private void OnValidate()
        {
            if (itemType == ItemType.Weapon)
            {
                if (weaponInfoSO == null)
                {
                    Debug.LogWarning("[WeaponSO is Null] Insert WeaponSO");
                    return;
                }
                id = weaponInfoSO.id;
                itemName = weaponInfoSO.weaponName;
                
            }
        }
    }
}
