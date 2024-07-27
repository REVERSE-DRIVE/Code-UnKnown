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
        [TextArea(3, 10)]
        public string description;
        public Sprite icon;

        public int goalValue;
    }

}