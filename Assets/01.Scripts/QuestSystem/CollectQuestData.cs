using ItemManage;

namespace QuestManage
{
    public class CollectQuestData : QuestData
    {
        public ItemType _itemType;
        public CollectQuestData(int id, int goal, ItemType itemType) : base(id, goal)
        {
            _itemType = itemType;
        }
    }
}