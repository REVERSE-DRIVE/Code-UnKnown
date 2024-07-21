using System;
using UnityEngine;

namespace QuestManage
{
    public class QuestObserver : MonoSingleton<QuestObserver>
    {
        [SerializeField] private QuestListSO _list;
        public QuestData[] currentQuestDatas;


        private void Awake()
        {
            // 저장된 데이터에서 현재 수락된 퀘스트들에 대한 정보를 모두 들고온다. (3개)
        }

        public void KillTrigger()
        {
            // EnemyType을 받아와서 킬 카운트 적립
            for (int i = 0; i < currentQuestDatas.Length; i++)
            {
                
            }
        }

        public void CollectTrigger()
        {
            // 여기도 ItemType같은거를 추가해서 구현해야될거같음
        }
    }
    
}