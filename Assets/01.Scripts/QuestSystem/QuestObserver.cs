using System;
using ItemManage;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        [SerializeField] private QuestListSO _list;
        public QuestData[] currentQuestDatas;


        private void Awake()
        {
            currentQuestDatas = QuestManager.Instance.AcceptQuestDatas.ToArray();
        }

        public void KillTrigger(EnemyType enemyType)
        {
            // EnemyType을 받아와서 킬 카운트 적립
            for (int i = 0; i < currentQuestDatas.Length; i++)
            {
                if (currentQuestDatas[i].id == (int)enemyType)
                {
                    currentQuestDatas[i].Trigger(1);
                }
            }
        }

        public void CollectTrigger(ItemType itemType)
        {
            // ItemType을 받아와서 아이템 카운트 적립
            for (int i = 0; i < currentQuestDatas.Length; i++)
            {
                if (currentQuestDatas[i].id == (int)itemType - 10)
                {
                    currentQuestDatas[i].Trigger(1);
                }
            }
        }
    }
    
}