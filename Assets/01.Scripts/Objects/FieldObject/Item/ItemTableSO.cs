using System.Collections.Generic;
using UnityEngine;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/ItemTableSO", fileName = "ItemTableSO")]
    public class ItemTableSO : ScriptableObject
    {
        public ItemType itemType;
        public List<ItemSO> itemSOList;
    }
}