using System;
using System.Collections.Generic;
using System.Linq;
using ItemManage;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        public List<QuestData> currentQuestDatas;
        public List<QuestListSO> currentQuestListSOs;
        

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        [ContextMenu("Debug")]
        public void DebugQuest()
        {
            KillTrigger(1, 10);
        }

        [ContextMenu("Apply")]
        public void ApplyAllQuest()
        {
            if (QuestManager.Instance == null) return;
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas;
            currentQuestListSOs = QuestManager.Instance.AcceptQuestListSOs;
        }
        public void KillTrigger(int index, int count)
        {
            var quest = GetQuestSO(index, QuestType.Kill);
            if (quest.goalValue <= count)
            {
                var data = currentQuestDatas.Find(x => x.id == currentQuestListSOs[index].id);
                data.Trigger(count);
            }
        }
        
        private QuestSO GetQuestSO(int index, QuestType type)
        {
            return currentQuestListSOs[index].questList.Find(x => x.questType == type);
        }
    }
}