using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    public class DailyQuestManager : MonoSingleton<DailyQuestManager>
    {
        [SerializeField] private QuestListSO[] _questTable;
        [SerializeField] private int _easyQuestCount;
        [SerializeField] private int _normalQuestCount;
        [SerializeField] private int _hardQuestCount;
        
        private List<QuestData> _dailyQuestDatas;
        
        private QuestSO GetDailyQuest(QuestDifficultyEnum difficulty)
        {
            var index = (int)difficulty - 1;
            return _questTable[index].GetRandomQuest();
        }

        /// <summary>
        /// Give daily quest to player
        /// </summary>
        [ContextMenu("GiveDailyQuest")]
        public void GiveDailyQuest()
        {
            _dailyQuestDatas = new List<QuestData>();
            for (int i = 0; i < _easyQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Easy);
                _dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue, quest.difficulty));
            }
            
            for (int i = 0; i < _normalQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Normal);
                _dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue, quest.difficulty));
            }
            
            for (int i = 0; i < _hardQuestCount; i++)
            {
                var quest = GetDailyQuest(QuestDifficultyEnum.Hard);
                _dailyQuestDatas.Add(new QuestData(quest.id, quest.goalValue, quest.difficulty));
            }
        }
        
        private bool IsQuestClear(int index)
        {
            return _dailyQuestDatas[index].isClear;
        }
        
        private bool IsAllQuestClear()
        {
            for (int i = 0; i < _dailyQuestDatas.Count; i++)
            {
                if (!IsQuestClear(i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}