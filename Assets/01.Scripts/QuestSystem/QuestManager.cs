using System;
using System.Collections.Generic;
using DG.Tweening;
using QuestManage;
using UnityEngine;

namespace QuestManage
{
    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField] private QuestListSO _questListSO;
        [SerializeField] private QuestWindowUI[] _questWindowUI;
        [SerializeField] private QuestUI _questUI;
        
        private List<QuestSO> _currentQuests = new List<QuestSO>();


        private void GetRandomQuest(int index)
        {
            var randomQuest = _questListSO.GetRandomQuest();
            _currentQuests.Add(randomQuest);
            _questWindowUI[index].SetQuest(randomQuest);
        }

        private void SpawnQuestWindow()
        {
            _questUI.OpenQuestWindow();
        }
        
        [ContextMenu("SettingQuestWindow")]
        private void SettingQuestWindow()
        {
            for (int i = 0; i < _questWindowUI.Length; i++)
            {
                GetRandomQuest(i);
            }
        }

    }
}