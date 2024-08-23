using System.Collections.Generic;
using UnityEngine;

namespace QuestManage
{
    [CreateAssetMenu(menuName = "SO/Quest/QuestListSO")]
    public class QuestListSO : ScriptableObject
    {
        public int id;
        public string title;
        public QuestDifficultyEnum difficulty;
        [TextArea(3, 10)]
        public string description;
        public Sprite icon;
        public List<QuestSO> questList;
    }
}