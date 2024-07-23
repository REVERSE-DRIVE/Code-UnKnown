using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    
    public class QuestSO : ScriptableObject
    {
        public int id;
        public string title;
        public QuestDifficultyEnum difficulty;
        public string description;

        public int goalValue;
    }

}