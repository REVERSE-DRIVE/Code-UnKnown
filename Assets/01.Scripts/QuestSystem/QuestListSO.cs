using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    [CreateAssetMenu(menuName = "SO/Quest/QuestListSO")]
    public class QuestListSO : ScriptableObject
    {
        public List<QuestSO> questList;

        public QuestSO FineQuest(int id, QuestDifficultyEnum difficulty)
        {
            return questList.Find(quest => quest.id == id && quest.difficulty == difficulty);
        }
        
        public QuestSO GetRandomQuest()
        {
            return questList[Random.Range(0, questList.Count)];
        }
    }
}