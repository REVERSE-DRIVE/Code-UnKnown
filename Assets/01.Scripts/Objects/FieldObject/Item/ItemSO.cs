using System;
using PlayerPartsManage;
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
        public int resourceValue;
        public PlayerPartDataSO partDataSO;

        private void OnValidate()
        {
            if (itemType == ItemType.Part)
            {
                if (partDataSO == null)
                {
                    Debug.LogWarning("[PartSO is Null] Insert PartSO");
                    return;
                }
                id = partDataSO.id;
                itemName = partDataSO.partName;
            }
        }
    }
}
