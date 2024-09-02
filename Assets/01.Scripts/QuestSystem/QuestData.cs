﻿using System;
using UnityEngine;

namespace QuestManage
{
    
    [System.Serializable]
    public class QuestData
    {
        public int id;
        public int completenessValue;
        public QuestDifficultyEnum difficulty;
        public bool isClear;
        [SerializeField] private int _goalValue;
        
        public Action OnClearEvent;

        public QuestData(int id, int goal, QuestDifficultyEnum difficulty)
        {
            this.id = id;
            this._goalValue = goal;
            this.difficulty = difficulty;
        }
    
        public void Trigger(int value)
        {
            completenessValue += value;
            if (completenessValue >= _goalValue)
            {
                // 클리어 이벤트
                isClear = true;
                OnClearEvent?.Invoke();
            }
        }
    }
}
