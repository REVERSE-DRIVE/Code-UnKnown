using System;
using System.Collections.Generic;
using ItemManage;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        public List<QuestData> currentQuestDatas;
        public List<QuestListSO> currentQuestListSOs;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        [ContextMenu("Apply")]
        public void ApplyAllQuest()
        {
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas;
            currentQuestListSOs = QuestManager.Instance.AcceptQuestListSOs;
        }

        [ContextMenu("TestKillTrigger")]
        private void Test()
        {
            KillTrigger(EnemyType.Decoy, 1);
        }

        public void KillTrigger(EnemyType enemyType, int triggerValue)
        {
            // EnemyType을 받아와서 킬 카운트 적립
            for (int i = 0; i < currentQuestListSOs.Count; i++)
            {
                for (int j = 0; j < currentQuestListSOs[i].questList.Count; j++)
                {
                    if (currentQuestListSOs[i].questList[j] is KillQuestSO killQuestSO)
                    {
                        if (killQuestSO.enemyType == enemyType)
                        {
                            currentQuestDatas[i].Trigger(triggerValue);
                        }
                    }
                }
            }
        }

        public void CollectTrigger(ItemType itemType)
        {
            // ItemType을 받아와서 아이템 카운트 적립
            for (int i = 0; i < currentQuestListSOs.Count; i++)
            {
                for (int j = 0; j < currentQuestListSOs[i].questList.Count; j++)
                {
                    if (currentQuestListSOs[i].questList[j] is CollectQuestSO collectQuestSO)
                    {
                        if (collectQuestSO.itemType == itemType)
                        {
                            currentQuestDatas[i].Trigger(1);
                        }
                    }
                }
            }
        }
    }
    
    
    
}