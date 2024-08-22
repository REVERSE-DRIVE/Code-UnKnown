using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField] private QuestListSO _questListSO;
        [SerializeField] private QuestWindowUI[] _questWindowUI;
        [SerializeField] private QuestUI _questUI;
        
        private List<QuestSO> _currentQuests = new List<QuestSO>();
        [field:SerializeField] public List<QuestData> AcceptQuestDatas { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /**
         * 퀘스트를 랜덤하게 설정
         */
        private void GetRandomQuest(int index)
        {
            var randomQuest = _questListSO.GetRandomQuest();
            _currentQuests.Add(randomQuest);
            _questWindowUI[index].SetQuest(randomQuest);
        }

        /**
         * 퀘스트 UI 오픈
         */
        public void SpawnQuestWindow()
        {
            _questUI.OpenQuestWindow();
        }
        
        /**
         * 퀘스트 UI 속 퀘스트 설정
         */
        [ContextMenu("SettingQuestWindow")]
        private void SettingQuestWindow()
        {
            for (int i = 0; i < _questWindowUI.Length; i++)
            {
                GetRandomQuest(i);
            }
        }

        /**
         * 퀘스트 수락 메서드
         */
        public void AcceptQuest(QuestData getQuestData)
        {
            Debug.Log(GetQuestDescription(getQuestData.id, getQuestData.difficulty));
            AcceptQuestDatas.Add(getQuestData);
        }
        
        public string GetQuestDescription(int id, QuestDifficultyEnum difficulty)
        {
            var quest = FindQuest(id, difficulty);
            return quest.description;
        }
        
        public QuestSO FindQuest(int id, QuestDifficultyEnum difficulty)
        {
            return _questListSO.FineQuest(id, difficulty);
        }
    }
}