using UnityEngine;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/ItemSO", fileName = "ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int id;
        public string itemName;
        public Sprite itemIcon;
    }
}
