using System.Collections.Generic;
using QuestManage;
using UnityEngine;

namespace QuestManage
{
    [CreateAssetMenu(menuName = "SO/Quest/QuestTable")]
    public class QuestTableSO : ScriptableObject
    {
        public List<QuestListSO> questList;
        
        public QuestListSO GetQuestList(int id, QuestDifficultyEnum difficulty)
        {
            return questList.Find(quest => quest.id == id && quest.difficulty == difficulty);
        }

        public QuestListSO GetRandomQuestSO()
        {
            return questList[Random.Range(0, questList.Count)];
        } 
    }
}