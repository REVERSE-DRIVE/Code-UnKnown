using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        public List<QuestData> currentQuestDatas;
        public List<QuestListSO> currentQuestListSOs;
        
        public QuestCounter questCounter;
        

        protected override void Awake()
        {
        }
        
        [ContextMenu("Debug")]
        public void DebugQuest()
        {
            
        }

        [ContextMenu("Apply")]
        public void ApplyAllQuest()
        {
            if (QuestManager.Instance == null) return;
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas;
            currentQuestListSOs = QuestManager.Instance.AcceptQuestListSOs;
        }
        
        public void Trigger(QuestType type, int count)
        {
            for (int i = 0; i < currentQuestDatas.Count; i++)
            {
                var quest = GetQuestSO(i, type);
                if (quest == null) continue;
                if (quest.goalValue <= count && !quest.isClear)
                {
                    var data = currentQuestDatas.Find(x => x.id == currentQuestListSOs[i].id);
                    data.Trigger(quest.triggerValue);
                    quest.isClear = true;
                }
            }
            
        }

        [ContextMenu("Reset")]
        public void ResetQuest()
        {
            for (int i = 0; i < currentQuestListSOs.Count; i++)
            {
                var quest = currentQuestListSOs[i].questList;
                for (int j = 0; j < quest.Count; j++)
                {
                    quest[j].isClear = false;
                }
            }
        }

        private QuestSO GetQuestSO(int index, QuestType type)
        {
            return currentQuestListSOs[index].questList.Find(x => x.questType == type);
        }
    }
}