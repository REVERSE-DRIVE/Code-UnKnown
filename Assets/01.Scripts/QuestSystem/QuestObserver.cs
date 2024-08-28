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

        [ContextMenu("Apply")]
        public void ApplyAllQuest()
        {
            if (QuestManager.Instance == null) return;
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas;
            currentQuestListSOs = QuestManager.Instance.AcceptQuestListSOs;
        }
        
        public void CollectTrigger(ItemType type, int value)
        {
            foreach (var quest in currentQuestDatas)
            {
                if (quest.isClear) continue;
                if (quest.id != (int) type) continue;
                quest.Trigger(value);
            }
        }
        
        public void KillTrigger(int value)
        {
            foreach (var quest in currentQuestDatas)
            {
            }
        }
    }
}