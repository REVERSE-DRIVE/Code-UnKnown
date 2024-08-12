using ItemManage;

namespace QuestManage
{
    public class CollectQuestData : QuestData
    {
        public ItemType _itemType;

        public CollectQuestData(int id, int goal, ItemType type, QuestDifficultyEnum difficulty) : base(id, goal, difficulty)
        {
            _itemType = type;
        }
    }
}