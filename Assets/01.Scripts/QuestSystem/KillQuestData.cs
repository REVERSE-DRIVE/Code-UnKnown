using UnityEngine;

namespace QuestManage
{
    public class KillQuestData : QuestData
    {
        public EnemyType _enemyType;
        public KillQuestData(int id, int goal, EnemyType enemyType) : base(id, goal)
        {
            _enemyType = enemyType;
        }
    }
}