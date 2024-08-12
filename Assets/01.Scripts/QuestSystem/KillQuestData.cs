using UnityEngine;

namespace QuestManage
{
    public class KillQuestData : QuestData
    {
        public EnemyType _enemyType;
        public KillQuestData(int id, int goal, EnemyType enemyType, QuestDifficultyEnum difficulty) : base(id, goal, difficulty)
        {
            _enemyType = enemyType;
        }
    }
}