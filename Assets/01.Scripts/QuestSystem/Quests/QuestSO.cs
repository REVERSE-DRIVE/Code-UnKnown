using UnityEngine;

namespace QuestManage
{
    public class QuestSO : ScriptableObject
    {
        public int goalValue;
        public int triggerValue;
        public QuestType questType;
        public bool isClear;
    }
}