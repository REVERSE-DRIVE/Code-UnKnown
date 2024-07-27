using UnityEngine;

namespace QuestManage
{
    
    [System.Serializable]
    public class QuestData
    {
        public int id;
        public int progressValue;
        [SerializeField] private int _goalValue;

        public QuestData(int id, int goal)
        {
            this.id = id;
            this._goalValue = goal;
        }
    
        public void Trigger(int value)
        {
            progressValue += value;
            if (progressValue >= _goalValue)
            {
                // 클리어 이벤트
            }
        }
    }
}
