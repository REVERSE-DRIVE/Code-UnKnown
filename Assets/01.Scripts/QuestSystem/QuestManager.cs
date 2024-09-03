using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField] private QuestTableSO _questListSO;
        [SerializeField] private QuestWindowUI[] _questWindowUI;
        [SerializeField] private QuestUI _questUI;
        
        private List<QuestListSO> _currentQuests = new List<QuestListSO>();
        [field:SerializeField] public List<QuestData> AcceptQuestDatas { get; set; }
        [field:SerializeField] public List<QuestListSO> AcceptQuestListSOs { get; set; }

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SettingQuestWindow();
        }

        private void Update()
        {
            
        }

        /**
         * 퀘스트를 랜덤하게 설정
         */
        private void GetRandomQuest(int index)
        {
            var randomQuest = _questListSO.GetRandomQuestSO();
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
         * 퀘스트 UI 클로즈
         */
        public void CloseQuestWindow()
        {
            _questUI.CloseQuestWindow();
        }
        
        /**
         * 퀘스트 UI 속 퀘스트 설정
         */
        [ContextMenu("SettingQuestWindow")]
        private void SettingQuestWindow()
        {
            if (_questWindowUI == null) return;
            for (int i = 0; i < _questWindowUI.Length; i++)
            {
                GetRandomQuest(i);
            }
        }

        /**
         * 퀘스트 수락 메서드
         */
        public void AcceptQuest(QuestListSO listSO, QuestData getQuestData)
        {
            AcceptQuestDatas.Add(getQuestData);
            AcceptQuestListSOs.Add(listSO);
        }
        
        public QuestListSO FindQuestListSO(int id, QuestDifficultyEnum difficulty)
        {
            return _questListSO.GetQuestList(id, difficulty);
        }
    }
}