using System.Collections.Generic;
using ItemManage;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        public QuestData[] currentQuestDatas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        [ContextMenu("Apply")]
        private void ApplyAllQuest()
        {
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas.ToArray();
        }

        [ContextMenu("TestKillTrigger")]
        private void Test()
        {
            KillTrigger(EnemyType.Decoy);
        }

        public void KillTrigger(EnemyType enemyType)
        {
            // EnemyType을 받아와서 킬 카운트 적립
            for (int i = 0; i < currentQuestDatas.Length; i++)
            {
                if (currentQuestDatas[i] is KillQuestData killQuestData)
                {
                    if (killQuestData._enemyType == enemyType)
                    {
                        killQuestData.Trigger(1);
                    }
                }
            }
        }

        public void CollectTrigger(ItemType itemType)
        {
            // ItemType을 받아와서 아이템 카운트 적립
            for (int i = 0; i < currentQuestDatas.Length; i++)
            {
                if (currentQuestDatas[i] is CollectQuestData collectQuestData)
                {
                    if (collectQuestData._itemType == itemType)
                    {
                        collectQuestData.Trigger(1);
                    }
                }
            }
        }
    }
    
}