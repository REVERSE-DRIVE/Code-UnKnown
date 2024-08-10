using UnityEngine;

namespace QuestManage
{
    public class DailyQuestManager : MonoSingleton<DailyQuestManager>
    {
        [SerializeField] private QuestListSO[] _questTable;
        [SerializeField] private int _easyQuestCount;
        [SerializeField] private int _normalQuestCount;
        [SerializeField] private int _hardQuestCount;
        
        private QuestSO GetDailyQuest(QuestDifficultyEnum difficulty)
        {
            var index = (int)difficulty;
            return _questTable[index].GetRandomQuest();
        }

        /// <summary>
        /// Give daily quest to player
        /// </summary>
        public void GiveDailyQuest()
        {
            for (int i = 0; i < _easyQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Easy);
                QuestObserver.Instance.dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue));
            }
            
            for (int i = 0; i < _normalQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Normal);
                QuestObserver.Instance.dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue));
            }
            
            for (int i = 0; i < _hardQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Hard);
                QuestObserver.Instance.dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue));
            }
        }
    }
}